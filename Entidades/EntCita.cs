namespace FisioFlores.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EntAdminCita
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("especialista")]
        public string Especialista { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }

        [JsonProperty("estadoId")]
        public int EstadoId { get; set; }

        [JsonProperty("fecha")]
        public string Fecha { get; set; }

        [JsonProperty("hora")]
        public string Hora { get; set; }

        [JsonProperty("observacion")]
        public string Observacion { get; set; }

        [JsonProperty("paciente")]
        public string Paciente { get; set; }

        [JsonProperty("pacienteId")]
        public int PacienteId { get; set; }
    }
}
