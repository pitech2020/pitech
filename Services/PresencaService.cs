using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PiTech.API.Data;
using PiTech.API.Domain;

namespace PiTech.API.Services
{
    public class PresencaService : IPresencaService
    {
        private readonly DataContext _dataContext;

        public PresencaService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AtualizaPresencaAsync(Presenca presenca)
        {
            _dataContext.Presencas.Update(presenca);
            var linhas = await _dataContext.SaveChangesAsync();
            return linhas > 0;
        }

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

        public async Task<Presenca> ObterPresencaAsync(Guid id)
        {
            return await _dataContext.Presencas.SingleOrDefaultAsync(predicate:p => p.Id == id);
        }

        public async Task<List<Presenca>> ObterPresencasAsync()
        {
            return await _dataContext.Presencas.ToListAsync();
        }

        public async Task<bool> PresencaPertenceAoUsuarioAsync(Guid id, string userId)
        {
            var presenca = await _dataContext.Presencas.AsNoTracking().SingleOrDefaultAsync(predicate: p => p.Id == id);

            return presenca != null && presenca.UserId == userId;
        }
    }
}
