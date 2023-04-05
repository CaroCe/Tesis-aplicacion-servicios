using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            CitumEspecialista = new HashSet<Citum>();
            CitumUsuarios = new HashSet<Citum>();
            ComentarioForos = new HashSet<ComentarioForo>();
            Consulta = new HashSet<Consultum>();
            HistoriaClinicaEspecialista = new HashSet<HistoriaClinica>();
            HistoriaClinicaPacientes = new HashSet<HistoriaClinica>();
            HistorialCita = new HashSet<HistorialCitum>();
            HorarioEspecialista = new HashSet<HorarioEspecialistum>();
        }

        public int UsuarioId { get; set; }
        public int? LateralidadId { get; set; }
        public int? RolId { get; set; }
        public int? SedeId { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? UsuarioIdentificacion { get; set; }
        public DateTime? UsuarioFechaNacimiento { get; set; }
        public string? UsuarioDireccion { get; set; }
        public string? UsuarioTelefono { get; set; }
        public string? UsuarioCorreo { get; set; }
        public string? UsuarioPassword { get; set; }
        public string? UsuarioOcupacion { get; set; }
        public string? UsuarioProfesion { get; set; }
        public bool? UsuarioEstado { get; set; }

        public virtual Lateralidad? Lateralidad { get; set; }
        public virtual Rol? Rol { get; set; }
        public virtual Sede? Sede { get; set; }
        public virtual ICollection<Citum> CitumEspecialista { get; set; }
        public virtual ICollection<Citum> CitumUsuarios { get; set; }
        public virtual ICollection<ComentarioForo> ComentarioForos { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
        public virtual ICollection<HistoriaClinica> HistoriaClinicaEspecialista { get; set; }
        public virtual ICollection<HistoriaClinica> HistoriaClinicaPacientes { get; set; }
        public virtual ICollection<HistorialCitum> HistorialCita { get; set; }
        public virtual ICollection<HorarioEspecialistum> HorarioEspecialista { get; set; }
    }
}
