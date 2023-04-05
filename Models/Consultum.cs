using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Consultum
    {
        public Consultum()
        {
            Evolucions = new HashSet<Evolucion>();
            Foros = new HashSet<Foro>();
            FotosExaminacions = new HashSet<FotosExaminacion>();
            Tratamientos = new HashSet<Tratamiento>();
        }

        public int ConsultaId { get; set; }
        public int? EspecialistaId { get; set; }
        public int? HistoriaId { get; set; }
        public DateTime? ConsultaFecha { get; set; }
        public string? ConsultaMotivo { get; set; }
        public string? ConsultaDescripcion { get; set; }
        public byte[]? ConsultaImagen { get; set; }
        public string? ConsultaDescripImagen { get; set; }
        public string? ConsultaProblema { get; set; }
        public string? ExaminacionObservacion { get; set; }
        public string? ExaminacionInspeccion { get; set; }
        public string? Diagnostico { get; set; }

        public virtual Usuario? Especialista { get; set; }
        public virtual HistoriaClinica? Historia { get; set; }
        public virtual ICollection<Evolucion> Evolucions { get; set; }
        public virtual ICollection<Foro> Foros { get; set; }
        public virtual ICollection<FotosExaminacion> FotosExaminacions { get; set; }
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }
    }
}
