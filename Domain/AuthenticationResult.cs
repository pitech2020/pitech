using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
