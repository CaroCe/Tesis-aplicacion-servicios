using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class FotosExaminacion
    {
        public int FotoExaminacionId { get; set; }
        public int? ConsultaId { get; set; }
        public byte[]? FotoExaminacionImagen { get; set; }
        public string? FotoExaminacionDescripcion { get; set; }

        public virtual Consultum? Consulta { get; set; }
    }
}
