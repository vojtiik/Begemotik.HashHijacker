using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begemotik.Twitter.Writter
{
    public class StartStatus
    {
        /// <summary>
        /// Id of just created status
        /// </summary>
        public long StatusId { get; set; }

        /// <summary>
        /// Date time of created status 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// This is user long id 
        /// </summary>
        /// TODO : review id_str, resource says we should not use this long ig - https://dev.twitter.com/docs/platform-objects/users
        public long UserId { get; set; }

    }
}
