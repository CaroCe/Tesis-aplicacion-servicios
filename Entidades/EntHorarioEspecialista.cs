namespace FisioFlores.Entidades
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EntHorarioEspecialista
    {
        [JsonProperty("horarioDiaId")]
        public int HorarioDiaId { get; set; }

        [JsonProperty("horarioDiaNombre")]
        public string HorarioDiaNombre { get; set; }

        [JsonProperty("horarioTrabajo")]
        public List<HorarioTrabajo> HorarioTrabajo { get; set; }

        public EntHorarioEspecialista()
        {
            this.HorarioTrabajo = new List<HorarioTrabajo>();
        }
    }

    public partial class HorarioTrabajo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("horaDesde")]
        public string HoraDesde { get; set; }

        [JsonProperty("horaHasta")]
        public string HoraHasta { get; set; }
    }
}
