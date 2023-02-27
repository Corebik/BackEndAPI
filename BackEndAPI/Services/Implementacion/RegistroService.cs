using Microsoft.EntityFrameworkCore;
using BackEndAPI.Models;
using BackEndAPI.Services.Contrato;

namespace BackEndAPI.Services.Implementacion
{
    public class RegistroService : IRegistroService
    {
        private CobecaContext _dbContext;

        public RegistroService(CobecaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Registro>> GetList()
        {
            try 
            {
                List<Registro> lista = new List<Registro>();
                lista = await _dbContext.Registros.Include(rgt => rgt.RefCasaNavigation).ToListAsync();
                return lista;
            }
            catch(Exception ex) 
            { 
                throw ex; 
            }
        }

        public async Task<Registro> Get(int id_registro)
        {
            try
            {
                Registro? encontrado = new Registro();

                encontrado = await _dbContext.Registros.Include(rgt => rgt.RefCasaNavigation).Where(e => e.IdRegistro == id_registro).FirstOrDefaultAsync();
                return encontrado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Registro> Add(Registro modelo)
        {
            try
            {
                _dbContext.Registros.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Registro modelo)
        {
            try
            {
                _dbContext.Registros.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Registro modelo)
        {
            try
            {
                _dbContext.Registros.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
