using Presence.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AuthenticationResult> RegistrarAsync(string email, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        Task<AuthenticationResult> LoginAsync(string email, string senha);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
