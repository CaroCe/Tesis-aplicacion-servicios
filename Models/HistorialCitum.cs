using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class HistorialCitum
    {
        public int HistorialCitaId { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? CitaFecha { get; set; }
        public string? CitaHora { get; set; }
        public string? HistorialCitaObservacion { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
