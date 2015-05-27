using Begemotik.Core;
using Begemotik.User.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Begemotik.Twitter.Auth
{
    public class Authenticator
    {
        public static OAuthAccessToken ReadOAuthAccessToken(string username)
        {
            var user = UserRepositoryFactory.GetRepository().ReadUserAccessToken(username);

            // TODO ???? int/long userid in token vs userid in user
            return new OAuthAccessToken() { ScreenName = username, Token = user.Token, TokenSecret = user.TokenSecret, UserId = (int)user.UserId };
        }

        /// <summary>
        /// user signes in for the first time 
        /// </summary>
        /// <param name="callbackUrl"></param>
        /// <returns></returns>
        public static Uri SignMeInUsingTwitter(string callbackUrl)
        {
            // Step 1 - Retrieve an OAuth Request Token
            TwitterService service = new TwitterService(TwitterAccessCodes._consumerKey_writter, TwitterAccessCodes._consumerSecret_writter);

            // This is the registered callback URL
            OAuthRequestToken requestToken = service.GetRequestToken(callbackUrl);

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);

            return uri;
        }

        /// <summary>
        /// user is authenticated by twitter 
        /// </summary>
        /// <param name="oauth_token"></param>
        /// <param name="oauth_verifier"></param>
        /// <returns></returns>
        public static string SignMeInUsingTwitter_Callback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            // Step 3 - Exchange the Request Token for an Access Token
            TwitterService service = new TwitterService(TwitterAccessCodes._consumerKey_writter, TwitterAccessCodes._consumerSecret_writter);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            // store to DB
            UserRepositoryFactory.GetRepository().SaveUserAccessToken(user.Id, accessToken.Token, accessToken.TokenSecret, user.ScreenName);

            // return username all the rest of user info is in the DB
            return user.ScreenName;
        }
    }
}
