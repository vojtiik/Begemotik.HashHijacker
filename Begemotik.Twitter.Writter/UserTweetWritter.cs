using Begemotik.Core;
using Begemotik.Twitter.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Begemotik.Twitter.Writter
{
    public class UserTweetWritter
    {
        public static StartStatus TweetStart(string status, string username)
        {
            var accessToken = Authenticator.ReadOAuthAccessToken(username);

            TwitterService service = new TwitterService(TwitterAccessCodes._consumerKey_writter, TwitterAccessCodes._consumerSecret_writter);
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);

            var options = new SendTweetOptions();
            options.Status = status;
            var twitterStatus = service.SendTweet(options);

            // if there is no status return null else return start status
            return (twitterStatus == null) ?
                null : new StartStatus()
                {
                    StatusId = twitterStatus.Id,
                    CreatedDate = twitterStatus.CreatedDate,
                    UserId = twitterStatus.User.Id
                };
        }

    }
}
