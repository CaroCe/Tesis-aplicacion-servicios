using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class EjercicioTratamiento
    {
        public int EjercicioTratamientoId { get; set; }
        public int? TratamientoDiaId { get; set; }
        public int? EjercicioId { get; set; }
        public int? EjercicioTratamientoRepeticiones { get; set; }
        public int? EjercicioTratamientoSerie { get; set; }
        public bool? EjercicioEstado { get; set; }
        public string? EjercicioDescanso { get; set; }
        public string? EjercicioObservacion { get; set; }

        public virtual Ejercicio? Ejercicio { get; set; }
        public virtual TratamientoDium? TratamientoDia { get; set; }
    }
}
