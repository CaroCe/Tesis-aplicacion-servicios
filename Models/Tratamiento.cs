using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Tratamiento
    {
        public Tratamiento()
        {
            TratamientoDia = new HashSet<TratamientoDium>();
        }

        public int TratamientoId { get; set; }
        public int? ConsultaId { get; set; }
        public DateTime? TratamientoFechaCreacion { get; set; }
        public int? TratamientoDias { get; set; }
        public DateTime? TratamientoFechaInicio { get; set; }
        public string? TratamientoObservacion { get; set; }
        public string? TratamientoDescripcion { get; set; }
        public string? TratamientoRecomendacion { get; set; }
        public string? TratamientoFase { get; set; }
        public bool? TratamientoCompleto { get; set; }

        public virtual Consultum? Consulta { get; set; }
        public virtual ICollection<TratamientoDium> TratamientoDia { get; set; }
    }
}
