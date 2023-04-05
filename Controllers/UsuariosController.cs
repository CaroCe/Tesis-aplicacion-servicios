using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FisioFlores.Models;
using SagatFarmServices.Entidades;
using System.Security.Cryptography;
using System.Text;
using FisioFlores.Entidades;
using System.Net.Mail;

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public UsuariosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public dynamic GetUsuarios()
        {
            List<EntUsuario> listaRetorno = new List<EntUsuario>();
            if (_context.Usuarios == null)
            {
              return NotFound();
            }
            List<Usuario> lista = new List<Usuario>();
            lista = _context.Usuarios.ToList();

            
            lista.ForEach(c =>
            {
                HistoriaClinica itemH = new HistoriaClinica();
                try
                {
                    itemH = _context.HistoriaClinicas.Where(x => x.PacienteId == c.UsuarioId).ToList()[0];
                }
                catch (Exception)
                {
                    itemH.HistoriaId = 0;
                }
                
                EntUsuario item = new EntUsuario
                {
                    LateralidadId = c.LateralidadId ?? 0,
                    UsuarioProfesion = c.UsuarioProfesion ?? "",
                    UsuarioOcupacion = c.UsuarioOcupacion ?? "",
                    RolId = c.RolId ?? 0,
                    SedeId = c.SedeId ?? 0,
                    UsuarioCorreo = c.UsuarioCorreo ?? "",
                    UsuarioDireccion = c.UsuarioDireccion ?? "",
                    UsuarioFechaNacimiento = c.UsuarioFechaNacimiento ?? DateTime.Now,
                    UsuarioId = c.UsuarioId,
                    UsuarioIdentificacion = c.UsuarioIdentificacion ?? "",
                    UsuarioNombre = c.UsuarioNombre ?? "",
                    UsuarioTelefono = c.UsuarioTelefono ??"",
                    UsuarioEstado = c.UsuarioEstado??false,
                    HistoriaId = itemH.HistoriaId

                };

                listaRetorno.Add(item);
            });

            return listaRetorno;

        }
        [HttpGet("Especialista")]
        public dynamic GetUsuariosEspecialista()
        {
            List<EntUsuario> listaRetorno = new List<EntUsuario>();
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            List<Usuario> lista = new List<Usuario>();
            lista = _context.Usuarios.Where(c=>c.RolId == 1).ToList();


            lista.ForEach(c =>
            {
                HistoriaClinica itemH = new HistoriaClinica();
                try
                {
                    itemH = _context.HistoriaClinicas.Where(x => x.PacienteId == c.UsuarioId).ToList()[0];
                }
                catch (Exception)
                {
                    itemH.HistoriaId = 0;
                }

                EntUsuario item = new EntUsuario
                {
                    LateralidadId = c.LateralidadId ?? 0,
                    UsuarioProfesion = c.UsuarioProfesion ?? "",
                    UsuarioOcupacion = c.UsuarioOcupacion ?? "",
                    RolId = c.RolId ?? 0,
                    SedeId = c.SedeId ?? 0,
                    UsuarioCorreo = c.UsuarioCorreo ?? "",
                    UsuarioDireccion = c.UsuarioDireccion ?? "",
                    UsuarioFechaNacimiento = c.UsuarioFechaNacimiento ?? DateTime.Now,
                    UsuarioId = c.UsuarioId,
                    UsuarioIdentificacion = c.UsuarioIdentificacion ?? "",
                    UsuarioNombre = c.UsuarioNombre ?? "",
                    UsuarioTelefono = c.UsuarioTelefono ?? "",
                    UsuarioEstado = c.UsuarioEstado ?? false,
                    HistoriaId = itemH.HistoriaId

                };

                listaRetorno.Add(item);
            });

            return listaRetorno;

        }
        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost("Filtro")]
        public dynamic GetUsuarioFiltro(EntFiltroUsuario item)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }

            List<EntUsuario> listaRetorno = new List<EntUsuario>();
            List<Usuario> usuario = new List<Usuario>();
            
            usuario = _context.Usuarios.ToList();

            usuario.ForEach(c =>
            {
                HistoriaClinica itemH = new HistoriaClinica();
                try
                {
                    itemH = _context.HistoriaClinicas.Where(x => x.PacienteId == c.UsuarioId).ToList()[0];
                }
                catch (Exception)
                {
                    itemH.HistoriaId = 0;
                }
                EntUsuario item = new EntUsuario();
                item.UsuarioId = c.UsuarioId;
                item.UsuarioNombre = c.UsuarioNombre??"";
                item.UsuarioIdentificacion = c.UsuarioIdentificacion ?? "";
                item.UsuarioFechaNacimiento = c.UsuarioFechaNacimiento?? DateTime.Now;
                item.UsuarioOcupacion = c.UsuarioOcupacion??"";
                item.UsuarioProfesion = c.UsuarioProfesion ?? "";
                item.UsuarioDireccion = c.UsuarioDireccion ?? "";
                item.LateralidadId = c.LateralidadId ?? 0;
                item.RolId = c.RolId ?? 0;
                item.SedeId = c.SedeId ?? 0;
                item.UsuarioCorreo = c.UsuarioCorreo ?? "";
                item.UsuarioTelefono = c.UsuarioTelefono ?? "";
                item.UsuarioEstado = c.UsuarioEstado ?? false;
                item.HistoriaId = itemH.HistoriaId;
                listaRetorno.Add(item);
            });



            if (usuario == null)
            {
                return NotFound();
            }




            return listaRetorno.Where(c=>
            c.UsuarioNombre.ToLower().Contains(item.Nombre.ToLower()) 
            && c.UsuarioIdentificacion.ToLower().Contains(item.Cedula.ToLower())
            && (c.SedeId == item.Sede || item.Sede == 0) 
            && (c.RolId == item.Rol || item.Rol == 0)
            );
        }
        private string transformarAString(string? valor)
        {
            return valor??"";
        }
        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, EntUsuario item)
        {



            Usuario usuario = new Usuario();
            
            usuario = _context.Usuarios.Find(id)!;

            usuario.UsuarioNombre = item.UsuarioNombre;
            usuario.UsuarioProfesion = item.UsuarioProfesion;
            usuario.UsuarioId = item.UsuarioId;
            usuario.UsuarioCorreo = item.UsuarioCorreo;
            usuario.LateralidadId = item.LateralidadId==0?null:item.LateralidadId;
            usuario.RolId = item.RolId;
            usuario.SedeId = item.SedeId==0?null:item.SedeId;
            usuario.UsuarioDireccion = item.UsuarioDireccion;
            usuario.UsuarioIdentificacion = item.UsuarioIdentificacion;
            usuario.UsuarioOcupacion = item.UsuarioOcupacion;
            usuario.UsuarioTelefono = item.UsuarioTelefono;
            usuario.UsuarioFechaNacimiento = item.UsuarioFechaNacimiento;
            usuario.UsuarioEstado = item.UsuarioEstado;
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.UsuarioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("RegistroPaciente")]
        public dynamic RegistoUsuario(EntRegistro registro)
        {
            Usuario usuario = new Usuario();
            usuario.UsuarioNombre = registro.Nombre;
            usuario.UsuarioCorreo = registro.Email;
            usuario.UsuarioDireccion = registro.Domicilio;
            usuario.UsuarioFechaNacimiento = registro.FechaNacimiento;
            usuario.UsuarioTelefono = registro.Telefono;
            usuario.UsuarioIdentificacion = registro.Cedula;
            usuario.UsuarioPassword = ComputeMd5Hash(registro.Password);
            usuario.RolId = registro.RolId;
            
            usuario.UsuarioEstado = true;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            registro.Id = usuario.UsuarioId;

            return registro;
        
        }
        [HttpGet("EnviarCodigo/{email}/{codigo}")]
        public dynamic EnviarCodigo(string email,string codigo)
        {
            string EmailOrigen = "floresfisioterapia.fsdev@gmail.com";
            string Password = "gpregtdzwqwrevjt";

            MailMessage oMailMessage = new MailMessage(EmailOrigen,email, "Código de verificación", "El código es: " + codigo);
            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient();
            oSmtpClient.EnableSsl = false;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587;
            
            try
            {
                oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Password);
                oSmtpClient.Send(oMailMessage);
                oSmtpClient.Dispose();
            }
            catch(Exception e)
            {
                return e;
            }
            
            return Ok();
        }
        [HttpGet("ResetPassword/{email}/{password}")]
        public dynamic ResetearPassword(string email, string password)
        {

            Usuario itemUsuario = new Usuario();
            itemUsuario = _context.Usuarios.Where(u=>u.UsuarioCorreo!.CompareTo(email) == 0).First();

            itemUsuario.UsuarioPassword = ComputeMd5Hash(password);
            try
            {
                _context.Usuarios.Update(itemUsuario);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            

            return Ok();
        }
        [HttpGet("Entrar/{email}/{password}")]
        public dynamic EntrarUsuario(string email, string password)
        {

            Usuario itemUsuario = new Usuario();
            try
            {
                itemUsuario = _context.Usuarios.Where(c => c.UsuarioCorreo==email && c.UsuarioPassword==ComputeMd5Hash(password) && Convert.ToBoolean(c.UsuarioEstado)).ToList()[0];
            }
            catch (Exception)
            {
                return BadRequest();
            }
            EntLogin retorno = new EntLogin();
            List<RolPermiso> lista = new List<RolPermiso>();
            lista = _context.RolPermisos.Where(c=>c.RolId == itemUsuario.RolId).Include(r=>r.Permiso).ToList();
            retorno.Nombre = itemUsuario.UsuarioNombre;
            retorno.Email = itemUsuario.UsuarioCorreo;
            retorno.Id = itemUsuario.UsuarioId;
            lista = lista.OrderBy(c=>c.Permiso.PermisoPadre).ToList();
            while (lista.Count>0)
            {
                int id = lista[0].PermisoId;
                if (lista[0].Permiso?.PermisoPadre == 0)
                {
                    Menu iteMenu = new Menu();
                    iteMenu.Nombre = lista[0].Permiso?.PermisoNombre;
                    List<RolPermiso> listaSubMenu = new List<RolPermiso>();
                    listaSubMenu = lista.Where(c=>c.Permiso?.PermisoPadre == lista[0].PermisoId).ToList();
                    while (listaSubMenu.Count>0)
                    {
                        int permisoId = listaSubMenu[0].PermisoId;
                        SubMenu itemSubMenu = new SubMenu();
                        itemSubMenu.Icon = listaSubMenu[0].Permiso?.PermisoIcon;
                        itemSubMenu.Url = listaSubMenu[0].Permiso?.PermisoRuta;
                        itemSubMenu.Nombre = listaSubMenu[0].Permiso?.PermisoNombre;

                        iteMenu.SubMenu.Add(itemSubMenu);
                        listaSubMenu.RemoveAll(c => c.PermisoId == permisoId);
                        lista.RemoveAll(c => c.PermisoId == permisoId);
                    }
                    lista.RemoveAll(c => c.PermisoId == id);
                    iteMenu.SubMenu = iteMenu.SubMenu.OrderBy(c => c.Nombre).ToList();
                    retorno.Menu.Add(iteMenu);
                }
            }
            retorno.Menu = retorno.Menu.OrderBy(c => c.Nombre).ToList();
            return Ok(retorno);
        }
        [HttpGet("ValidarCorreo/{email}")]
        public bool ValidarCorreo(string email)
        {
            return (_context.Usuarios?.Any(e=>e.UsuarioCorreo!.CompareTo(email)==0)).GetValueOrDefault();
        }
        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }
        public static string ComputeMd5Hash(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
