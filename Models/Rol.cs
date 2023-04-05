using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolPermisos = new HashSet<RolPermiso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int RolId { get; set; }
        public string? RolNombre { get; set; }

        public virtual ICollection<RolPermiso> RolPermisos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
