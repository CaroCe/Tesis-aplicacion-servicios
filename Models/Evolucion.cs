using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Evolucion
    {
        public Evolucion()
        {
            FotosEvolucions = new HashSet<FotosEvolucion>();
        }

        public int EvolucionId { get; set; }
        public int? ConsultaId { get; set; }
        public string? EvolucionDescripcion { get; set; }
        public DateTime? EvolucionFecha { get; set; }

        public virtual Consultum? Consulta { get; set; }
        public virtual ICollection<FotosEvolucion> FotosEvolucions { get; set; }
    }
}
