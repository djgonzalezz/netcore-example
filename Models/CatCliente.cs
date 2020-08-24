using System;

namespace pruebadeacero.Models
{
    public class CatCliente
    {
        public int ClaCliente { get; set; }
        public string NomCliente { get; set; }
        public Guid ApiKey { get; set; }
        public string Descripcion { get; set; }
    }
}