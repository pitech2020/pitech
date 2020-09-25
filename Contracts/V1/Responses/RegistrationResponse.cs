using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Contracts.V1.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthSuccessResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
