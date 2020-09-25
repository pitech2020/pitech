using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Presence.API.Data;
using Presence.API.Domain;

namespace Presence.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PresencaService : IPresencaService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly DataContext _dataContext;

        /// <summary>
        /// 
        /// </summary>
        public PresencaService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="presenca"></param>
        /// <returns></returns>
        public async Task<bool> AtualizaPresencaAsync(Presenca presenca)
        {
            _dataContext.Presencas.Update(presenca);
            var linhas = await _dataContext.SaveChangesAsync();
            return linhas > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> DeletarPresencaAsync(Guid id)
        {
            var presenca = await ObterPresencaAsync(id);

            if(presenca == null)
            {
                return false;
            }

            _dataContext.Presencas.Remove(presenca);
            var linhas = await _dataContext.SaveChangesAsync();

            return linhas > 0;
        }

        public async Task<bool> CriarPresencaAsync(Presenca presenca)
        {
            await _dataContext.Presencas.AddAsync(presenca);
            var linhas = await _dataContext.SaveChangesAsync();
            return linhas > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Presenca> ObterPresencaAsync(Guid id)
        {
            return await _dataContext.Presencas.SingleOrDefaultAsync(predicate:p => p.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Presenca>> ObterPresencasAsync()
        {
            return await _dataContext.Presencas.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> PresencaPertenceAoUsuarioAsync(Guid id, string userId)
        {
            var presenca = await _dataContext.Presencas.AsNoTracking().SingleOrDefaultAsync(predicate: p => p.Id == id);

            return presenca != null && presenca.UserId == userId;
        }
    }
}
