using System;
namespace Finding_a_Tournament.Domain.dto.Responses
{
    public class ClubsResponse
    {
        public int IdClub { get; set; }
        public string NombreClub { get; set; }
        public string Direccion  { get; set; }
        public string TelefonoContacto { get; set; }
        public string Diciplina { get; set; }
    }
}