using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class HorarioDia
    {
        public HorarioDia()
        {
            HorarioEspecialista = new HashSet<HorarioEspecialistum>();
        }

        public int HorarioId { get; set; }
        public string? HorarioNombre { get; set; }

        public virtual ICollection<HorarioEspecialistum> HorarioEspecialista { get; set; }
    }
}
