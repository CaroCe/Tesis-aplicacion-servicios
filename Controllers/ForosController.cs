using FisioFlores.Entidades;
using FisioFlores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public ForosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }
        // GET: api/<ForosController>
        [HttpPost("Filtro")]
        public dynamic PostFiltroForo(Filtro filtro)
        {
            if (_context.Consulta == null)
            {
                return NotFound();
            }
            List<EntConsulta> listaRetorno = new List<EntConsulta>();
            List<Consultum> lista = new List<Consultum>();
            List<Usuario> listaUsuarios = new List<Usuario>();

            listaUsuarios = _context.Usuarios.ToList();

            lista = _context.Consulta.Include(c => c.Historia).ToList();

            List<Foro> listaForos = new List<Foro>();
            listaForos = _context.Foros.ToList();

            if (filtro.Problema.Length > 0)
            {
                lista = lista.Where(c => c.ConsultaProblema?.ToLower().Contains(filtro.Problema.ToLower()) ?? false).ToList();
            }


            byte[] bytes = new byte[1024];

            lista.ForEach(c =>
            {
                EntConsulta consulta = new EntConsulta();
                consulta.ConsultaImagen = Encoding.UTF8.GetString(c.ConsultaImagen ?? bytes);
                consulta.ConsultaId = c.ConsultaId;
                consulta.EspecialistaId = c.EspecialistaId ?? 0;
                consulta.HistoriaId = c.HistoriaId ?? 0;
                consulta.ForoId = listaForos.Find(f => f.ConsultaId == c.ConsultaId)?.ForoId??0;
                consulta.ForoEstado = consulta.ForoId>0?true:false;
                consulta.PacienteId = c.Historia?.PacienteId ?? 0;
                consulta.PacienteNombre = listaUsuarios.First(lu => lu.UsuarioId == consulta.PacienteId).UsuarioNombre ?? "";
                consulta.ConsultaFecha = new DateTime(c.ConsultaFecha?.Year ?? 1, c.ConsultaFecha?.Month ?? 1, c.ConsultaFecha?.Day ?? 1);
                consulta.ConsultaMotivo = c.ConsultaMotivo ?? "";
                consulta.ConsultaDescripcion = c.ConsultaDescripcion ?? "";
                consulta.ConsultaDescripImagen = c.ConsultaDescripImagen ?? "";
                consulta.ConsultaProblema = c.ConsultaProblema ?? "";
                consulta.ExaminacionObservacion = c.ExaminacionObservacion ?? "";
                consulta.ExaminacionInspeccion = c.ExaminacionInspeccion ?? "";
                consulta.Diagnostico = c.Diagnostico ?? "";
                List<FotosExaminacion> listaFotos = new List<FotosExaminacion>();
                listaFotos = _context.FotosExaminacions.Where(c => c.ConsultaId == consulta.ConsultaId).ToList();
                listaFotos.ForEach(f =>
                {
                    List<byte> tmp = new List<byte>();
                    EntFotoExaminacion foto = new EntFotoExaminacion();
                    foto.FotoExaminacionId = f.FotoExaminacionId;
                    foto.FotoExaminacionDescripcion = f.FotoExaminacionDescripcion ?? "";
                    foto.FotoExaminacionImagen = Encoding.UTF8.GetString(f.FotoExaminacionImagen ?? tmp.ToArray());
                    consulta.fotosExaminacion.Add(foto);
                });
                listaRetorno.Add(consulta);
            });
            return listaRetorno;


        }

        // GET api/<ForosController>/5
        [HttpGet("CambiarEstado/{consultaId}/{estado}")]
        public dynamic Get(int consultaId,bool estado)
        {
            

            if (estado)
            {
                Foro item = new Foro();
                item.ConsultaId = consultaId;

                _context.Foros.Add(item);
                _context.SaveChanges();
                return item;
            }
            else
            {
                
                var consulta = _context.Foros.First(c=>c.ConsultaId == consultaId);
                if (consulta == null)
                {
                    return NotFound();
                }

                var comentarios = _context.ComentarioForos.Where(k => k.ForoId == consulta.ForoId);
                _context.ComentarioForos.RemoveRange(comentarios);
                _context.SaveChanges();
                _context.Foros.Remove(consulta);
                _context.SaveChanges();

                return NoContent();
            }

            
        }

        // POST api/<ForosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpGet("ComentarioForo/{foroId}")]
        public dynamic GetComentariosForo(int foroId)
        {
            List<ComentarioForoEntidad> lista = new List<ComentarioForoEntidad>();
            var usuarios = _context.Usuarios.ToList();

            _context.ComentarioForos
                .Where(c=>c.ForoId == foroId).ToList().ForEach(c =>
                {
                    ComentarioForoEntidad item = new ComentarioForoEntidad();
                    item.ComentarioForoId = c.ComentarioForoId;
                    item.ComentarioForoMensaje = c.ComentarioForoMensaje!;
                    item.Foro = c.Foro!;
                    item.Usuario = usuarios.FirstOrDefault(c => c.UsuarioId == c.UsuarioId)!.UsuarioNombre!;
                    item.UsuarioId = c.UsuarioId??0;
                    item.ForoId = c.ForoId??0;
                    lista.Add(item);
                });
            return lista;

        }

        [HttpPost("ComentarioForo")]
        public dynamic PostComentariosForo([FromBody] ComentarioForo item)
        {
            _context.ComentarioForos.Add(item);
            _context.SaveChanges();

            return item;
        }


        // PUT api/<ForosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ForosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
