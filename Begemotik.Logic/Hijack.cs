using Begemotik.Core;
using Begemotik.Twitter.Writter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begemotik.Twitter.Logic
{
    public class HashHijack
    {
        /// <summary>
        /// uniqly identifies thi hijack
        /// </summary>
        public long HashHijackId { get; set; }
        
        /// <summary>
        /// Id of start up tweet
        /// </summary>
        public long SinceStatusId { get; set; }

        /// <summary>
        /// Id of end tweet, (optional for now not sure if there is any end tweet might be expensive)
        /// </summary>
        public long MaxStatusId { get; set; }

        /// <summary>
        /// When hijack starts (comes from start up tweet)
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// When hijack ends (comes from completion tweet)
        /// </summary>
        public DateTime FinishDate { get; set; }

        /// <summary>
        /// Id of a user responsible for this hash hijack
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status"></param>
        /// <param name="username"></param>
        public HashHijack(string status, string username, HashHijackPeriod period)
        {
            try
            {
                Start(status, username, period);
            }
            catch (ArgumentException arge)
            { }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Creates hijack within the bigemotik and gets current tweet id
        /// </summary>
        /// <returns></returns>
        private void Start(string status, string username, HashHijackPeriod period)
        {
            // user tweets about starting
            StartStatus startStatus = UserTweetWritter.TweetStart(status, username);

            // create DB entry
            if (startStatus != null)
            {
                SinceStatusId = startStatus.StatusId;
                StartDate = startStatus.CreatedDate;
                FinishDate = Conversions.ToFinishDate(StartDate, period);
                UserId = startStatus.UserId;
            }

            // set id of this task 
            HashHijackId = 5;
            // send hijack to msmq for processing
        }

        /// <summary>
        /// Terminates hijack within bigemotik and gets closing tweet id
        /// </summary>
        /// <returns></returns>
        public int Finish()
        {
            return 0;
        }

    }
}
