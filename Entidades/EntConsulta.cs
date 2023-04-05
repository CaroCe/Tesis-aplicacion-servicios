namespace FisioFlores.Entidades
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EntConsulta
    {
        [JsonProperty("consultaId")]
        public int ConsultaId { get; set; }

        [JsonProperty("especialistaId")]
        public int EspecialistaId { get; set; }

        [JsonProperty("pacienteId")]
        public int PacienteId { get; set; }

        [JsonProperty("foroId")]
        public int ForoId { get; set; }

        [JsonProperty("foroEstado")]
        public bool ForoEstado { get; set; }

        [JsonProperty("pacienteNombre")]
        public string PacienteNombre { get; set; }

        [JsonProperty("historiaId")]
        public int HistoriaId { get; set; }

        [JsonProperty("consultaFecha")]
        public DateTime ConsultaFecha { get; set; }

        [JsonProperty("consultaMotivo")]
        public string ConsultaMotivo { get; set; }

        [JsonProperty("consultaDescripcion")]
        public string ConsultaDescripcion { get; set; }

        [JsonProperty("consultaImagen")]
        public string ConsultaImagen { get; set; }

        [JsonProperty("consultaDescripImagen")]
        public string ConsultaDescripImagen { get; set; }

        [JsonProperty("consultaProblema")]
        public string ConsultaProblema { get; set; }

        [JsonProperty("examinacionObservacion")]
        public string ExaminacionObservacion { get; set; }

        [JsonProperty("examinacionInspeccion")]
        public string ExaminacionInspeccion { get; set; }

        [JsonProperty("diagnostico")]
        public string Diagnostico { get; set; }

        public List<EntFotoExaminacion> fotosExaminacion { get; set; }

        public EntConsulta()
        {
            this.fotosExaminacion = new List<EntFotoExaminacion>();
        }
    }
    public partial class EntFotoExaminacion
    {
        [JsonProperty("fotoExaminacionId")]
        public int FotoExaminacionId { get; set; }

        [JsonProperty("fotoExaminacionImagen")]
        public string FotoExaminacionImagen { get; set; }

        [JsonProperty("fotoExaminacionDescripcion")]
        public string FotoExaminacionDescripcion { get; set; }
    }
    public partial class EntFotoConsulta
    {
        [JsonProperty("fotoExaminacionId")]
        public int FotoExaminacionId { get; set; }

        [JsonProperty("consultaId")]
        public int ConsultaId { get; set; }

        [JsonProperty("fotoExaminacionImagen")]
        public string FotoExaminacionImagen { get; set; }

        [JsonProperty("fotoExaminacionDescripcion")]
        public string FotoExaminacionDescripcion { get; set; }
    }
    public partial class ComentarioForoEntidad
    {
        [JsonProperty("comentarioForoId")]
        public int ComentarioForoId { get; set; }

        [JsonProperty("comentarioForoMensaje")]
        public string ComentarioForoMensaje { get; set; }

        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonProperty("foroId")]
        public int ForoId { get; set; }

        [JsonProperty("foro")]
        public object Foro { get; set; }

        [JsonProperty("usuario")]
        public string Usuario { get; set; }
    }
}
