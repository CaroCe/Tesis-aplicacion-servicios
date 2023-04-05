using Newtonsoft.Json;

public partial class EntFotoEvolucion
{
    [JsonProperty("fotoEvolucionId")]
    public int FotoEvolucionId { get; set; }

    [JsonProperty("evolucionId")]
    public int EvolucionId { get; set; }

    [JsonProperty("fotoEvolucionImagen")]
    public string FotoEvolucionImagen { get; set; }

    [JsonProperty("fotoEvolucionDescripcion")]
    public string FotoEvolucionDescripcion { get; set; }
}