using System;

namespace Finding_a_Tournament.Domain.dto.Requests
{
    public class ClubsCreateRequest
    {
        public string NombreClub { get; set; }
        public string Direccion { get; set; }
        public string TelefonoContacto { get; set; }
        public double Geoubicacion { get; set; }
        public string Logotipo { get; set; }
        public string Diciplina { get; set; }
        public DateTime HorarioDiciplina { get; set; }
        public int CantidadPer { get; set; }
        public bool PersHabilidadesDiferentes { get; set; }
    }
}