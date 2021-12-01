using System;

namespace Finding_a_Tournament.Domain.dto.Responses
{
        public record PersonCreateDto(string NombreClub ,string Direccion, string TelefonoContacto, double Geoubicacion,string Logotipo ,string Diciplina ,DateTime HorarioDiciplina,int CantidadPer,bool PersHabilidadesDiferentes);

}