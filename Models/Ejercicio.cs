using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Ejercicio
    {
        public Ejercicio()
        {
            EjercicioTratamientos = new HashSet<EjercicioTratamiento>();
        }

        public int EjercicioId { get; set; }
        public string? EjercicioNombre { get; set; }
        public string? EjercicioGrafico { get; set; }
        public string? EjercicioDescripcion { get; set; }
        public bool? EjercicioEstado { get; set; }

        public virtual ICollection<EjercicioTratamiento> EjercicioTratamientos { get; set; }
    }
}
