namespace FisioFlores.Entidades
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EntTratamiento
    {
        [JsonProperty("tratamientoId")]
        public int TratamientoId { get; set; }

        [JsonProperty("consultaId")]
        public int ConsultaId { get; set; }

        [JsonProperty("tratamientoFechaCreacion")]
        public DateTime TratamientoFechaCreacion { get; set; }

        [JsonProperty("tratamientoDias")]
        public int TratamientoDias { get; set; }

        [JsonProperty("tratamientoFechaInicio")]
        public DateTime TratamientoFechaInicio { get; set; }

        [JsonProperty("tratamientoObservacion")]
        public string TratamientoObservacion { get; set; }

        [JsonProperty("tratamientoDescripcion")]
        public string TratamientoDescripcion { get; set; }

        [JsonProperty("tratamientoRecomendacion")]
        public string TratamientoRecomendacion { get; set; }

        [JsonProperty("tratamientoFase")]
        public string TratamientoFase { get; set; }

        [JsonProperty("tratamientoCompleto")]
        public bool TratamientoCompleto { get; set; }

        [JsonProperty("tratamientoDia")]
        public List<TratamientoDia> TratamientosDia { get; set; }
        public EntTratamiento()
        {
            this.TratamientosDia = new List<TratamientoDia>();
        }
    }

    public partial class TratamientoDia
    {
        [JsonProperty("tratamientoDiaId")]
        public int TratamientoDiaId { get; set; }

        [JsonProperty("tratamientoId")]
        public int TratamientoId { get; set; }

        [JsonProperty("tratamientoDiaFecha")]
        public DateTime TratamientoDiaFecha { get; set; }

        [JsonProperty("ejercicioTratamientos")]
        public List<EjercicioTratamientos> EjercicioTratamientos { get; set; }

        public TratamientoDia()
        {
            this.EjercicioTratamientos = new List<EjercicioTratamientos>();
        }
    }

    public partial class EjercicioTratamientos
    {
        [JsonProperty("ejercicioTratamientoId")]
        public int EjercicioTratamientoId { get; set; }

        [JsonProperty("tratamientoDiaId")]
        public int TratamientoDiaId { get; set; }

        [JsonProperty("ejercicioId")]
        public int EjercicioId { get; set; }

        [JsonProperty("ejercicioTratamientoRepeticiones")]
        public int EjercicioTratamientoRepeticiones { get; set; }

        [JsonProperty("ejercicioTratamientoSerie")]
        public int EjercicioTratamientoSerie { get; set; }

        [JsonProperty("ejercicioEstado")]
        public bool EjercicioEstado { get; set; }

        [JsonProperty("ejercicioDescanso")]
        public string EjercicioDescanso { get; set; }

        [JsonProperty("ejercicioObservacion")]
        public string EjercicioObservacion { get; set; }

        [JsonProperty("ejercicio")]
        public Ejercicio Ejercicio { get; set; }

        public EjercicioTratamientos()
        {
            this.Ejercicio = new Ejercicio();
        }
    }

    public partial class Ejercicio
    {
        [JsonProperty("ejercicioId")]
        public int EjercicioId { get; set; }

        [JsonProperty("ejercicioNombre")]
        public string EjercicioNombre { get; set; }

        [JsonProperty("ejercicioGrafico")]
        public string EjercicioGrafico { get; set; }

        [JsonProperty("ejercicioDescripcion")]
        public string EjercicioDescripcion { get; set; }

        [JsonProperty("ejercicioEstado")]
        public bool EjercicioEstado { get; set; }
    }
}
