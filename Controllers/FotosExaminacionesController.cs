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

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosExaminacionesController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public FotosExaminacionesController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/FotosExaminaciones
        [HttpGet]
        public dynamic GetFotosExaminacions()
        {
          if (_context.FotosExaminacions == null)
          {
              return NotFound();
          }
            List<FotosExaminacion> lista = new List<FotosExaminacion>();
            List<EntFotoConsulta> listaRetorno = new List<EntFotoConsulta>();
            lista = _context.FotosExaminacions.ToList();

            lista.ForEach(c =>
            {
                List<byte> tmp = new List<byte>(); 
                EntFotoConsulta item = new EntFotoConsulta();
                item.FotoExaminacionDescripcion = c.FotoExaminacionDescripcion??"";
                item.FotoExaminacionImagen = c.FotoExaminacionImagen is null?"":Encoding.UTF8.GetString(c.FotoExaminacionImagen ?? tmp.ToArray());
                item.ConsultaId = c.ConsultaId??0;
                item.FotoExaminacionId = c.FotoExaminacionId;
                listaRetorno.Add(item);
            });

            return listaRetorno;
        }

        // GET: api/FotosExaminaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FotosExaminacion>> GetFotosExaminacion(int id)
        {
          if (_context.FotosExaminacions == null)
          {
              return NotFound();
          }
            var fotosExaminacion = await _context.FotosExaminacions.FindAsync(id);

            if (fotosExaminacion == null)
            {
                return NotFound();
            }

            return fotosExaminacion;
        }

        // PUT: api/FotosExaminaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotosExaminacion(int id, EntFotoConsulta item)
        {
            FotosExaminacion fotosExaminacion = new FotosExaminacion();
            fotosExaminacion.FotoExaminacionId = item.FotoExaminacionId;
            fotosExaminacion.FotoExaminacionDescripcion = item.FotoExaminacionDescripcion;
            fotosExaminacion.ConsultaId = item.ConsultaId;
            fotosExaminacion.FotoExaminacionImagen = Encoding.ASCII.GetBytes(item.FotoExaminacionImagen);
            if (id != fotosExaminacion.FotoExaminacionId)
            {
                return BadRequest();
            }

            _context.Entry(fotosExaminacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotosExaminacionExists(id))
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

        // POST: api/FotosExaminaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public dynamic PostFotosExaminacion(EntFotoConsulta item)
        {
            FotosExaminacion fotosExaminacion = new FotosExaminacion();
            var fotosExa = _context.FotosExaminacions.Find(item.FotoExaminacionId);
            if (fotosExa == null)
            {
                
                fotosExaminacion.FotoExaminacionDescripcion = item.FotoExaminacionDescripcion;
                fotosExaminacion.ConsultaId = item.ConsultaId;
                fotosExaminacion.FotoExaminacionImagen = Encoding.ASCII.GetBytes(item.FotoExaminacionImagen);
                if (_context.FotosExaminacions == null)
                {
                    return Problem("Entity set 'bdd_fisio_floresContext.FotosExaminacions'  is null.");
                }
                _context.FotosExaminacions.Add(fotosExaminacion);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (FotosExaminacionExists(fotosExaminacion.FotoExaminacionId))
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
                _context.FotosExaminacions.Remove(fotosExa);
                _context.SaveChanges();
                fotosExaminacion.FotoExaminacionDescripcion = item.FotoExaminacionDescripcion;
                fotosExaminacion.ConsultaId = item.ConsultaId;
                fotosExaminacion.FotoExaminacionImagen = Encoding.ASCII.GetBytes(item.FotoExaminacionImagen);
                if (_context.FotosExaminacions == null)
                {
                    return Problem("Entity set 'bdd_fisio_floresContext.FotosExaminacions'  is null.");
                }
                _context.FotosExaminacions.Add(fotosExaminacion);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (FotosExaminacionExists(fotosExaminacion.FotoExaminacionId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            

            

            return fotosExaminacion;
        }

        // DELETE: api/FotosExaminaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFotosExaminacion(int id)
        {
            if (_context.FotosExaminacions == null)
            {
                return NotFound();
            }
            var fotosExaminacion = await _context.FotosExaminacions.FindAsync(id);
            if (fotosExaminacion == null)
            {
                return NotFound();
            }

            _context.FotosExaminacions.Remove(fotosExaminacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotosExaminacionExists(int id)
        {
            return (_context.FotosExaminacions?.Any(e => e.FotoExaminacionId == id)).GetValueOrDefault();
        }
    }
}
