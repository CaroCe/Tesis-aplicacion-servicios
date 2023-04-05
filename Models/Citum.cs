using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Citum
    {
        public int CitaId { get; set; }
        public int? UsuarioId { get; set; }
        public int? EspecialistaId { get; set; }
        public DateTime? CitaFecha { get; set; }
        public string? CitaHora { get; set; }
        public string? CitaObservacion { get; set; }
        public int? CitaEstado { get; set; }

        public virtual Usuario? Especialista { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
