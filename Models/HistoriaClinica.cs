using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class HistoriaClinica
    {
        public HistoriaClinica()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int HistoriaId { get; set; }
        public int? PacienteId { get; set; }
        public int? EspecialistaId { get; set; }
        public string? HistoriaFuente { get; set; }
        public string? HistoriaAntecedentes { get; set; }
        public string? HistoriaPatologicos { get; set; }
        public string? HistoriaHabitos { get; set; }
        public string? HistoriaVivienda { get; set; }
        public string? HistoriaAlergias { get; set; }
        public string? HistoriaActFisica { get; set; }
        public DateTime? HistoriaFecha { get; set; }

        public virtual Usuario? Especialista { get; set; }
        public virtual Usuario? Paciente { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
