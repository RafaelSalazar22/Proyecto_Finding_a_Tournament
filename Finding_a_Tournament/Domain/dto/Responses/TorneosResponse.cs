using System;
namespace Finding_a_Tournament.Domain.dto.Responses
{
    public class TorneosResponse
    {
        public string NombreTorneo { get; set; }
        public int CantidadEquipos { get; set; }
        public string TipoTorneo { get; set; }
        public bool AcepHabilidadesdistintas { get; set; }
    }
}