using PiTech.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiTech.API.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegistrarAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string senha);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
