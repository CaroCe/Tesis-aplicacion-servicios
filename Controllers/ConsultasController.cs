using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FisioFlores.Models;
using FisioFlores.Entidades;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public ConsultasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Consultas
        [HttpGet]
        public dynamic GetConsulta()
        {
            if (_context.Consulta == null)
            {
                return NotFound();
            }
            List<EntConsulta> listaRetorno = new List<EntConsulta>();
            List<Consultum> lista = new List<Consultum>();
            lista = _context.Consulta.ToList();
            byte[] bytes = new byte[1024];

            lista.ForEach(c =>
            {
                EntConsulta consulta = new EntConsulta();
                consulta.ConsultaImagen = Encoding.UTF8.GetString(c.ConsultaImagen ?? bytes);
                consulta.ConsultaId = c.ConsultaId;
                consulta.EspecialistaId = c.EspecialistaId??0;
                consulta.HistoriaId = c.HistoriaId??0;
                consulta.ConsultaFecha = c.ConsultaFecha ?? new DateTime();
                consulta.ConsultaMotivo = c.ConsultaMotivo??"";
                consulta.ConsultaDescripcion = c.ConsultaDescripcion??"";
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
        private DateTime generarFecha(DateTime? fecha)
        {
            return new DateTime(fecha?.Year ?? 2022, fecha?.Month ?? 1, fecha?.Day ?? 1);
        }
        [HttpPost("Filtros")]
        public dynamic GetConsultaFiltros(Filtro filtro)
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

            
            if (filtro.PacienteId != 0)
            {
                lista = lista.Where(c => c.Historia?.PacienteId == filtro.PacienteId).ToList();
            }
            if (filtro.Problema.Length > 0)
            {
                lista = lista.Where(c => c.ConsultaProblema?.ToLower().Contains(filtro.Problema.ToLower()) ??false).ToList();
            }

            
            byte[] bytes = new byte[1024];

            lista.ForEach(c =>
            {
                EntConsulta consulta = new EntConsulta();
                consulta.ConsultaImagen = Encoding.UTF8.GetString(c.ConsultaImagen ?? bytes);
                consulta.ConsultaId = c.ConsultaId;
                consulta.EspecialistaId = c.EspecialistaId ?? 0;
                consulta.HistoriaId = c.HistoriaId ?? 0;
                consulta.PacienteId = c.Historia?.PacienteId??0;
                consulta.PacienteNombre = listaUsuarios.First(lu => lu.UsuarioId == consulta.PacienteId).UsuarioNombre??"";
                consulta.ConsultaFecha = new DateTime(c.ConsultaFecha?.Year??1,c.ConsultaFecha?.Month??1,c.ConsultaFecha?.Day??1);
                consulta.ConsultaMotivo = c.ConsultaMotivo ?? "";
                consulta.ConsultaDescripcion = c.ConsultaDescripcion ?? "";
                consulta.ConsultaDescripImagen = c.ConsultaDescripImagen ?? "";
                consulta.ConsultaProblema = c.ConsultaProblema ?? "";
                consulta.ExaminacionObservacion = c.ExaminacionObservacion ?? "";
                consulta.ExaminacionInspeccion = c.ExaminacionInspeccion ?? "";
                consulta.Diagnostico = c.Diagnostico ?? "";
                List<FotosExaminacion> listaFotos = new List<FotosExaminacion>();
                listaFotos = _context.FotosExaminacions.Where(c=>c.ConsultaId == consulta.ConsultaId).ToList();
                listaFotos.ForEach(f =>
                {
                    List<byte> tmp = new List<byte>();
                    EntFotoExaminacion foto = new EntFotoExaminacion();
                    foto.FotoExaminacionId = f.FotoExaminacionId;
                    foto.FotoExaminacionDescripcion = f.FotoExaminacionDescripcion??"";
                    foto.FotoExaminacionImagen = Encoding.UTF8.GetString(f.FotoExaminacionImagen ?? tmp.ToArray());
                    consulta.fotosExaminacion.Add(foto);
                });

                listaRetorno.Add(consulta);
            });



            return listaRetorno.Where(c=>c.ConsultaFecha>=filtro.FechaDesde && c.ConsultaFecha <= filtro.FechaHasta);
        }

        // GET: api/Consultas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consultum>> GetConsultum(int id)
        {
          if (_context.Consulta == null)
          {
              return NotFound();
          }
            var consultum = await _context.Consulta.FindAsync(id);

            if (consultum == null)
            {
                return NotFound();
            }

            return consultum;
        }

        // PUT: api/Consultas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsultum(int id, Consultum consultum)
        {
            if (id != consultum.ConsultaId)
            {
                return BadRequest();
            }

            _context.Entry(consultum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultumExists(id))
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

        // POST: api/Consultas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public dynamic PostConsultum(EntConsulta item)
        {
            Consultum consultum = new Consultum();
            consultum.ConsultaId = item.ConsultaId;
            consultum.ConsultaFecha = item.ConsultaFecha;
            consultum.ConsultaProblema = item.ConsultaProblema;
            consultum.ConsultaDescripImagen = item.ConsultaDescripImagen;
            consultum.ConsultaDescripcion = item.ConsultaDescripcion;
            consultum.ConsultaProblema = item.ConsultaProblema;
            consultum.ConsultaMotivo = item.ConsultaMotivo;
            consultum.ExaminacionObservacion = item.ExaminacionObservacion;
            consultum.ExaminacionInspeccion = item.ExaminacionInspeccion;
            consultum.Diagnostico = item.Diagnostico;
            consultum.EspecialistaId = item.EspecialistaId;
            consultum.HistoriaId = item.HistoriaId;
            consultum.ConsultaImagen=Encoding.ASCII.GetBytes(item.ConsultaImagen);
            
          if (_context.Consulta == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Consulta'  is null.");
          }
            if (consultum.ConsultaId == 0)
            {
                _context.Consulta.Add(consultum);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (ConsultumExists(consultum.ConsultaId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                _context.Entry(consultum).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultumExists(consultum.ConsultaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            

            return consultum.ConsultaId;
        }

        // DELETE: api/Consultas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultum(int id)
        {
            if (_context.Consulta == null)
            {
                return NotFound();
            }
            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum == null)
            {
                return NotFound();
            }

            _context.Consulta.Remove(consultum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultumExists(int id)
        {
            return (_context.Consulta?.Any(e => e.ConsultaId == id)).GetValueOrDefault();
        }
    }
}
