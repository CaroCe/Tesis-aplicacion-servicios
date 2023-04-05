using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class ComentarioForo
    {
        public int ComentarioForoId { get; set; }
        public string? ComentarioForoMensaje { get; set; }
        public int? UsuarioId { get; set; }
        public int? ForoId { get; set; }

        public virtual Foro? Foro { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
