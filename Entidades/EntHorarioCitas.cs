namespace FisioFlores.Entidades
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EntHorarioCita
    {
        [JsonProperty("horarioDiaId")]
        public int HorarioDiaId { get; set; }

        [JsonProperty("horarioDiaNombre")]
        public string HorarioDiaNombre { get; set; }

        [JsonProperty("horarioDiaFecha")]
        public DateTime HorarioDiaFecha { get; set; }

        [JsonProperty("horarioCitas")]
        public List<HorarioCita> HorarioCitas { get; set; }
        public EntHorarioCita()
        {
            this.HorarioCitas = new List<HorarioCita>();
        }
    }

    public partial class HorarioCita
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("horaCita")]
        public string HoraCita { get; set; }

        [JsonProperty("disponibles")]
        public int Disponibles { get; set; }
    }

    public partial class Horas
    {
        public int hora { get; set; }
        public int minuto { get; set; }
    }

    public partial class EntCita
    {
        [JsonProperty("citaId")]
        public long CitaId { get; set; }

        [JsonProperty("usuarioId")]
        public long UsuarioId { get; set; }

        [JsonProperty("citaFecha")]
        public DateTimeOffset CitaFecha { get; set; }

        [JsonProperty("citaHora")]
        public string CitaHora { get; set; }

        [JsonProperty("citaEstado")]
        public long CitaEstado { get; set; }
    }



}
