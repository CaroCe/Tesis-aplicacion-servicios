namespace SagatFarmServices.Entidades
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EntRegistro
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        [JsonProperty("fechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("domicilio")]
        public string Domicilio { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("rolId")]
        public int RolId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
    public partial class EntLogin
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("nombre")]
        public string? Nombre { get; set; }

        [JsonProperty("menu")]
        public List<Menu> Menu { get; set; }

        public EntLogin()
        {
            Menu = new List<Menu>();
        }
    }

    public partial class Menu
    {
        [JsonProperty("nombre")]
        public string? Nombre { get; set; }

        [JsonProperty("subMenu")]
        public List<SubMenu> SubMenu { get; set; }
        public Menu()
        {
            SubMenu = new List<SubMenu>();
        }
    }

    public partial class SubMenu
    {
        [JsonProperty("nombre")]
        public string? Nombre { get; set; }

        [JsonProperty("icon")]
        public string? Icon { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
    public partial class EntRolPermiso
    {
        [JsonProperty("rolId")]
        public int RolId { get; set; }

        [JsonProperty("permiso")]
        public int Permiso { get; set; }
    }
}
