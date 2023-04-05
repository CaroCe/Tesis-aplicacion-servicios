using System;
using System.Collections.Generic;

namespace FisioFlores.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            RolPermisos = new HashSet<RolPermiso>();
        }

        public int PermisoId { get; set; }
        public string? PermisoNombre { get; set; }
        public string? PermisoRuta { get; set; }
        public string? PermisoIcon { get; set; }
        public int? PermisoPadre { get; set; }

        public virtual ICollection<RolPermiso> RolPermisos { get; set; }
    }
}
