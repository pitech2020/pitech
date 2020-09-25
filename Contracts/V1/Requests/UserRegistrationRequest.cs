using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Contracts.V1.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRegistrationRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Senha { get; set; }
    }
}
