using System.Collections.Generic;
using System.Threading.Tasks;
using pruebadeacero.Models;

namespace pruebadeacero.IServices
{
    public interface ICatClienteService
    {
        List<CatCliente> GetList();
        CatCliente GetCatCliente(int claCliente);
        List<dynamic> GetCatClienteUsers();
        Task<string> CallApi();
    }
}