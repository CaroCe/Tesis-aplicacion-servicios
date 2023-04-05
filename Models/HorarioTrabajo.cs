using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class HorarioTrabajo
    {
        public HorarioTrabajo()
        {
            HorarioEspecialista = new HashSet<HorarioEspecialistum>();
        }

        public int HorarioTrabajoId { get; set; }
        public string? HorarioTrabajoDesde { get; set; }
        public string? HorarioTrabajoHasta { get; set; }

        public virtual ICollection<HorarioEspecialistum> HorarioEspecialista { get; set; }
    }
}
