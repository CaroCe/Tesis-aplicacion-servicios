using Newtonsoft.Json;

namespace FisioFlores.Entidades
{
    public class EntFiltro
    {
        public int sedeId { get; set; }
        public int especialistaId { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
    }
    public partial class Filtro
    {
        [JsonProperty("fechaDesde")]
        public DateTime? FechaDesde { get; set; }

        [JsonProperty("fechaHasta")]
        public DateTime? FechaHasta { get; set; }

        [JsonProperty("pacienteId")]
        public int PacienteId { get; set; }

        [JsonProperty("estado")]
        public int Estado { get; set; }

        [JsonProperty("problema")]
        public string Problema { get; set; }

        [JsonProperty("especialistaId")]
        public int EspecialistaId { get; set; }

        public Filtro()
        {
            this.Problema = "";
        }
    }

    public partial class FiltroConsulta
    {
        [JsonProperty("fechaDesde")]
        public DateTime FechaDesde { get; set; }

        [JsonProperty("fechaHasta")]
        public DateTime FechaHasta { get; set; }

        [JsonProperty("pacienteId")]
        public int PacienteId { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        public FiltroConsulta()
        {
            this.Cedula = "";
        }
    }
}
