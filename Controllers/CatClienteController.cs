using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pruebadeacero.IServices;
using pruebadeacero.Models;

namespace pruebadeacero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatClienteController : ControllerBase
    {
        private ICatClienteService _catClienteService;

        public CatClienteController(ICatClienteService catClienteService)
        {     
            _catClienteService = catClienteService;
        } 
        
        [HttpGet("getall")]
        public IList<CatCliente> GetAll()
        {
            var result = _catClienteService.GetList();
            return result;
        }

        [HttpGet("getClienteUsers")]
        public IList<dynamic> GetCatClienteUsers()
        {
            var result = _catClienteService.GetCatClienteUsers();
            return result;
        }

        [HttpGet("getkrakeninfo")]
        public async Task<string> getKrakenInfo()
        {
            var result = await _catClienteService.CallApi();
            return result;
        }

        [HttpGet("{claCliente}")]
        public CatCliente Get([Required]int claCliente)
        {
            var result = _catClienteService.GetCatCliente(claCliente);
            return result;
        }
    }
}