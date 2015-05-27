using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begemotik.Twitter.Auth
{
    public class AuthenticateUser
    {
        /// <summary>
        /// Screen name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// unique identifier
        /// </summary>
        public long UserId { get; set; }
    }
}
