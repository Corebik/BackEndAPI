using BackEndAPI.Models;
namespace BackEndAPI.Services.Contrato
{
    public interface ICasaService
    {
        Task<List<Casa>> GetList();
    }
}
