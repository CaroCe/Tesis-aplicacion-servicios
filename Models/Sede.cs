using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Sede
    {
        public Sede()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int SedeId { get; set; }
        public string? SedeNombre { get; set; }
        public string? SedeDireccion { get; set; }
        public string? SedeHoraDesde { get; set; }
        public string? SedeHoraHasta { get; set; }
        public string? SedeTelefono { get; set; }
        public bool? SedeEstado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
