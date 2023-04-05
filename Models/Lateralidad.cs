using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Lateralidad
    {
        public Lateralidad()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int LateralidadId { get; set; }
        public string? LateralidadNombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
