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
    public interface IPresencaService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Presenca>> ObterPresencasAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presenca"></param>
        /// <returns></returns>
        Task<bool> CriarPresencaAsync(Presenca presenca);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Presenca> ObterPresencaAsync(Guid id);
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="presenca"></param>
        /// <returns></returns>
        Task<bool> AtualizaPresencaAsync(Presenca presenca);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        Task<bool> DeletarPresencaAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> PresencaPertenceAoUsuarioAsync(Guid id, string userId);
    }
}
