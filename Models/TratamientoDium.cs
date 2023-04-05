using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class TratamientoDium
    {
        public TratamientoDium()
        {
            EjercicioTratamientos = new HashSet<EjercicioTratamiento>();
        }

        public int TratamientoDiaId { get; set; }
        public int? TratamientoId { get; set; }
        public DateTime? TratamientoDiaFecha { get; set; }

        public virtual Tratamiento? Tratamiento { get; set; }
        public virtual ICollection<EjercicioTratamiento> EjercicioTratamientos { get; set; }
    }
}
