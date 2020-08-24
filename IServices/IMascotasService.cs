using System.Collections.Generic;
using pruebadeacero.Models;

namespace pruebadeacero.IServices
{
    public interface IMascotasService
    {
        Mascota Save(Mascota mascota);
        List<Mascota> Get();
        Mascota Update(Mascota idMascota);
        string Delete(int idMascota);
    }
}