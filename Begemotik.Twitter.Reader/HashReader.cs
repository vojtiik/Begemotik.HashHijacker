using Begemotik.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Begemotik.Twitter.Reader
{
    public class HashReader
    {
        // TODO move to app config
        string _consumerKey = TwitterAccessCodes._consumerKey_Reader;
        string _consumerSecret = TwitterAccessCodes._consumerSecret_Reader;
        string _accessToken = TwitterAccessCodes._accessToken_Reader;
        string _accessSecret = TwitterAccessCodes._accessSecret_Reader;

        public HashReader()
        {
            // Pass your credentials to the service
            TwitterService service = new TwitterService(_consumerKey, _consumerSecret, _accessToken, _accessSecret);

            // Step 1 - Retrieve an OAuth Request Token
          //  OAuthRequestToken requestToken = service.GetRequestToken();

            // Step 2 - Redirect to the OAuth Authorization URL
            //Uri uri = service.GetAuthorizationUri(requestToken);
            //Process.Start(uri.ToString());

            //// Step 3 - Exchange the Request Token for an Access Token
           // string verifier = "3899713"; // <-- This is input into your application by your user
           // OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

            // Step 4 - User authenticates using the Access Token
            //service.AuthenticateWith(access.Token, access.TokenSecret);
            IEnumerable<TwitterStatus> mentions = service.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions());

            // search hashtags
            var searchOption = new SearchOptions();
          
            // this is maximum
            searchOption.Count = 100;
            
            // we want most recent only 
            searchOption.Resulttype = TwitterSearchResultType.Recent;
            
            // since we read for the last time
            searchOption.SinceId = null;

            // what to search for
                searchOption.Q = "#FF";

            // not implemented to be used for location based 
            //searchOption.geocode 

            TwitterSearchResult searchResults = service.Search(searchOption);

            // start before an hour
            var hijackStart = DateTime.Now.AddHours(-1); 

            // tweet here to get start id (and we go forwards instead)
            // this comes to user timeline

            
            // work backwards to find first votes
            // allow 480 calls (limit) and TODO : include timeout of 15 minutes
            for (int i = 480; i > 0; i--)
            {
                if (searchResults.Statuses.Any(status => status.CreatedDate < hijackStart))
                {
                    break;
                }

                // find the oldest and get next 100
                searchResults.Statuses.OrderBy(status => status.CreatedDate).First();
            }

        }
    }
}
