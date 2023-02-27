using BackEndAPI.Models;
namespace BackEndAPI.Services.Contrato
{
    public interface IRegistroService
    {
        Task<List<Registro>> GetList();
        Task<Registro> Get(int id_registro);
        Task<Registro> Add(Registro modelo);
        Task<bool> Update(Registro modelo);
        Task<bool> Delete(Registro modelo);
    }
}
