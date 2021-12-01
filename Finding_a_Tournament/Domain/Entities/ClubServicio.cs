using System;
using System.Collections.Generic;

#nullable disable

namespace Finding_a_Tournament.Domain.Entities
{
    public partial class ClubServicio
    {
        public int IdServicio { get; set; }
        public string Diciplina { get; set; }
        public DateTime HorarioDiciplina { get; set; }
        public int CantidadPer { get; set; }
        public bool PersHabilidadesDiferentes { get; set; }
        public int IdClub { get; set; }

        public virtual Club Club { get; set; }
    }
}
