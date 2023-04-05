using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class HorarioEspecialistum
    {
        public int HorarioEspecialistaId { get; set; }
        public int? EspecialistaId { get; set; }
        public int? HorarioId { get; set; }
        public int? HorarioTrabajoId { get; set; }
        public bool? HorarioEspecialistaEstado { get; set; }

        public virtual Usuario? Especialista { get; set; }
        public virtual HorarioDia? Horario { get; set; }
        public virtual HorarioTrabajo? HorarioTrabajo { get; set; }
    }
}
