using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using pruebadeacero.IServices;
using pruebadeacero.Models;

namespace pruebadeacero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MascotasController : ControllerBase
    {
        private IMascotasService _mascotasService;

        public MascotasController(IMascotasService mascotasService)
        {     
            _mascotasService = mascotasService;
        } 
        
        [HttpGet]
        public IList<Mascota> Get()
        {
            var result = _mascotasService.Get();
            return result;
        }

        [HttpPost]
        public Mascota Save([FromBody] Mascota mascota)
        {
            var result = _mascotasService.Save(mascota);
            return result;
        }

        [HttpPut("{idMascota}")]
        public Mascota Update([FromBody] Mascota mascota, [Required]int idMascota)
        {
            mascota.IdMascota = idMascota;
            var result = _mascotasService.Update(mascota);
            return result;
        }

        [HttpDelete("{idMascota}")]
        public string Delete([Required]int idMascota)
        {
            var result = _mascotasService.Delete(idMascota);
            return result;
        }
    }
}