using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class FotosEvolucion
    {
        public int FotoEvolucionId { get; set; }
        public int? EvolucionId { get; set; }
        public byte[]? FotoEvolucionImagen { get; set; }
        public string? FotoEvolucionDescripcion { get; set; }

        public virtual Evolucion? Evolucion { get; set; }
    }
}
