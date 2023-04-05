using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Converters;
namespace FisioFlores.Entidades
{
    public partial class EntUsuario
    {
        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonProperty("lateralidadId")]
        public int LateralidadId { get; set; }

        [JsonProperty("rolId")]
        public int RolId { get; set; }

        [JsonProperty("sedeId")]
        public int SedeId { get; set; }
        [JsonProperty("historiaId")]
        public int HistoriaId { get; set; }

        [JsonProperty("usuarioNombre")]
        public string UsuarioNombre { get; set; }

        [JsonProperty("usuarioIdentificacion")]
        public string UsuarioIdentificacion { get; set; }

        [JsonProperty("usuarioFechaNacimiento")]
        public DateTime UsuarioFechaNacimiento { get; set; }

        [JsonProperty("usuarioDireccion")]
        public string UsuarioDireccion { get; set; }

        [JsonProperty("usuarioTelefono")]
        public string UsuarioTelefono { get; set; }

        [JsonProperty("usuarioCorreo")]
        public string UsuarioCorreo { get; set; }

        [JsonProperty("usuarioOcupacion")]
        public string UsuarioOcupacion { get; set; }

        [JsonProperty("usuarioProfesion")]
        public string UsuarioProfesion { get; set; }

        [JsonProperty("usuarioEstado")]
        public bool UsuarioEstado { get; set; }

    }
    public partial class EntFiltroUsuario
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        [JsonProperty("sede")]
        public int Sede { get; set; }

        [JsonProperty("rol")]
        public int Rol { get; set; }
    }
    public partial class EntUsuarioHistoria
    {
        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonProperty("lateralidadId")]
        public int LateralidadId { get; set; }

        [JsonProperty("lateralidadNombre")]
        public string LateralidadNombre { get; set; }

        [JsonProperty("rolId")]
        public int RolId { get; set; }

        [JsonProperty("sedeId")]
        public int SedeId { get; set; }
        [JsonProperty("historiaId")]
        public int HistoriaId { get; set; }

        [JsonProperty("usuarioNombre")]
        public string UsuarioNombre { get; set; }

        [JsonProperty("usuarioIdentificacion")]
        public string UsuarioIdentificacion { get; set; }

        [JsonProperty("usuarioFechaNacimiento")]
        public DateTime UsuarioFechaNacimiento { get; set; }

        [JsonProperty("usuarioDireccion")]
        public string UsuarioDireccion { get; set; }

        [JsonProperty("usuarioTelefono")]
        public string UsuarioTelefono { get; set; }

        [JsonProperty("usuarioCorreo")]
        public string UsuarioCorreo { get; set; }

        [JsonProperty("usuarioOcupacion")]
        public string UsuarioOcupacion { get; set; }

        [JsonProperty("usuarioProfesion")]
        public string UsuarioProfesion { get; set; }

        [JsonProperty("usuarioEstado")]
        public bool UsuarioEstado { get; set; }

        [JsonProperty("pacienteId")]
        public int PacienteId { get; set; }

        [JsonProperty("especialistaId")]
        public int EspecialistaId { get; set; }

        [JsonProperty("historiaFuente")]
        public string HistoriaFuente { get; set; }

        [JsonProperty("historiaAntecedentes")]
        public string HistoriaAntecedentes { get; set; }

        [JsonProperty("historiaPatologicos")]
        public string HistoriaPatologicos { get; set; }

        [JsonProperty("historiaHabitos")]
        public string HistoriaHabitos { get; set; }

        [JsonProperty("historiaVivienda")]
        public string HistoriaVivienda { get; set; }

        [JsonProperty("historiaAlergias")]
        public string HistoriaAlergias { get; set; }

        [JsonProperty("historiaActFisica")]
        public string HistoriaActFisica { get; set; }

        [JsonProperty("historiaFecha")]
        public DateTime HistoriaFecha { get; set; }

    }
    
}
