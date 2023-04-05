using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Foro
    {
        public Foro()
        {
            ComentarioForos = new HashSet<ComentarioForo>();
        }

        public int ForoId { get; set; }
        public int? ConsultaId { get; set; }

        public virtual Consultum? Consulta { get; set; }
        public virtual ICollection<ComentarioForo> ComentarioForos { get; set; }
    }
}
