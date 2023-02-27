using Microsoft.EntityFrameworkCore;
using BackEndAPI.Models;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implementacion
{
    public class CasaService : ICasaService
    {
        private CobecaContext _dbContext;

        public CasaService(CobecaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Casa>> GetList()
        {
            try
            {
                List<Casa> lista = new List<Casa>();
                lista = await _dbContext.Casas.ToListAsync();
                return lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
