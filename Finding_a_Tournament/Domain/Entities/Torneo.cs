using System;
using System.Collections.Generic;

#nullable disable

namespace Finding_a_Tournament.Domain.Entities
{
    public partial class Torneo
    {
        public int IdTorneo { get; set; }
        public string NombreTorneo { get; set; }
        public int CantidadParticipantes { get; set; }
        public int CantidadEquipos { get; set; }
        public string TipoTorneo { get; set; }
        public bool AcepHabilidadesdistintas { get; set; }
    }
}
