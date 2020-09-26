using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PiTech.API.Data;
using PiTech.API.Domain;
using PiTech.API.Options;

namespace PiTech.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _dataContext;

        public IdentityService(
            UserManager<IdentityUser> userManager,
            JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            DataContext dataContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _dataContext = dataContext;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string senha)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Usuário não encontrado." }
                };
            }

            var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, senha);

            if (!senhaCorreta)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Combinação usuario/senha não encontrados." }
                };
            }

            return await GerarResultadoDeAutenticacaoUsuarioAsync(usuario);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var tokenValidado = ObterPrincipalPeloToken(token);

            if (tokenValidado == null)
            {
                return new AuthenticationResult { Errors = new[] { "Token inválido" } };
            }

            var dataExpiracaoUnix = long.Parse(tokenValidado.Claims.Single(c  => c.Type == JwtRegisteredClaimNames.Exp).Value);
            var dataExpiracao = new DateTime(
                year:1970,
                month:1,
                day:1,
                hour: 0,
                minute: 0,
                second: 0,
                kind: DateTimeKind.Utc)
                .AddSeconds(dataExpiracaoUnix);

            if(dataExpiracao > DateTime.Now)
            {
                return new AuthenticationResult { Errors = new[] { "O token ainda não está expirado" } };
            }

            var jti = tokenValidado.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            var refreshTokenArmazenado = await _dataContext.RefreshTokens.SingleOrDefaultAsync(predicate: rt => rt.Token == refreshToken);

            var errosValidacao = VerificarErrosRefreshTokenArmazenado(refreshTokenArmazenado, jti);

            if (errosValidacao != null)
            {
                return errosValidacao;
            }

            refreshTokenArmazenado.Usado = true;
            _dataContext.RefreshTokens.Update(refreshTokenArmazenado);

            await _dataContext.SaveChangesAsync();

            var usuario = await _userManager.FindByIdAsync(tokenValidado.Claims.Single(c => c.Type == "id").Value);
            return await GerarResultadoDeAutenticacaoUsuarioAsync(usuario);
        }

        private AuthenticationResult VerificarErrosRefreshTokenArmazenado(RefreshToken refreshTokenArmazenado, string jti)
        {
            List<string> errors = new List<string>();

            if (refreshTokenArmazenado == null)
            {
                errors.Add("Este refresh token não existe");
            }

            if (DateTime.UtcNow > refreshTokenArmazenado.DataExpiracao)
            {
                errors.Add("Este refresh token está expirado");
            }

            if (refreshTokenArmazenado.NaoValidado)
            {
                errors.Add("Este refresh token está expirado");
            }

            if (refreshTokenArmazenado.Usado)
            {
                errors.Add("Este refresh token já foi usado");
            }

            if (refreshTokenArmazenado.JwtId != jti)
            {
                errors.Add("Este token não bate com o token Jwt");
            }

            if (errors.Any())
            {
                return new AuthenticationResult { Errors = errors };
            }

            else return null;
        }

        private ClaimsPrincipal ObterPrincipalPeloToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var tokenValidado);
                if (!JwtComAlgotimoValidoDeSeguranca(tokenValidado))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool JwtComAlgotimoValidoDeSeguranca(SecurityToken tokenDeSeguranca)
        {
            return (tokenDeSeguranca is JwtSecurityToken jwtSecurityToken) 
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<AuthenticationResult> RegistrarAsync(string email, string senha)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(email);

            if (usuarioExistente != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Já existe um usuário cadastrado com este e-mail." }
                };
            }

            var novoUsuario = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            var usuarioCriado = await _userManager.CreateAsync(novoUsuario, senha);

            if (!usuarioCriado.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = usuarioCriado.Errors.Select(err => err.Description)
                };
            }

            return await GerarResultadoDeAutenticacaoUsuarioAsync(novoUsuario);
        }

        private async Task<AuthenticationResult> GerarResultadoDeAutenticacaoUsuarioAsync(IdentityUser usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: usuario.Email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Email, value: usuario.Email),
                    new Claim(type: "id", value: usuario.Id),
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = usuario.Id,
                DataCriacao = DateTime.Now,
                DataExpiracao = DateTime.UtcNow.AddMonths(6)
            };

            await _dataContext.RefreshTokens.AddAsync(refreshToken);
            await _dataContext.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
