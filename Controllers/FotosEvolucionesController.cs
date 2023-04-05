using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FisioFlores.Models;
using System.Text;

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosEvolucionesController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public FotosEvolucionesController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/FotosEvoluciones
        [HttpGet]
        public dynamic GetFotosEvolucions()
        {
            List<EntFotoEvolucion> lista = new List<EntFotoEvolucion>();
            List<FotosEvolucion> listaFotos = new List<FotosEvolucion>();
            listaFotos = _context.FotosEvolucions.ToList();
            listaFotos.ForEach(f =>
            {
                List<byte> tmp = new List<byte>();
                EntFotoEvolucion item = new EntFotoEvolucion();
                item.FotoEvolucionDescripcion = f.FotoEvolucionDescripcion??"";
                item.FotoEvolucionId = f.FotoEvolucionId;
                item.EvolucionId = f.EvolucionId??0;
                item.FotoEvolucionImagen = Encoding.UTF8.GetString(f.FotoEvolucionImagen ?? tmp.ToArray());
                lista.Add(item);
            });
            return lista;
        }
        [HttpGet("PorEvolucionId/{id}")]
        public dynamic GetFotosEvolucions(int id)
        {
            List<EntFotoEvolucion> lista = new List<EntFotoEvolucion>();
            List<FotosEvolucion> listaFotos = new List<FotosEvolucion>();
            listaFotos = _context.FotosEvolucions.Where(c=>c.EvolucionId == id).ToList();
            listaFotos.ForEach(f =>
            {
                List<byte> tmp = new List<byte>();
                EntFotoEvolucion item = new EntFotoEvolucion();
                item.FotoEvolucionDescripcion = f.FotoEvolucionDescripcion ?? "";
                item.FotoEvolucionId = f.FotoEvolucionId;
                item.EvolucionId = f.EvolucionId ?? 0;
                item.FotoEvolucionImagen = Encoding.UTF8.GetString(f.FotoEvolucionImagen ?? tmp.ToArray());
                lista.Add(item);
            });
            return lista;
        }
        // GET: api/FotosEvoluciones/5
        [HttpGet("{id}")]
        public dynamic GetFotosEvolucion(int id)
        {

            FotosEvolucion itemBase = new FotosEvolucion();
            itemBase = _context.FotosEvolucions.Find(id)??new FotosEvolucion();
            List<byte> tmp = new List<byte>();
            EntFotoEvolucion item = new EntFotoEvolucion();
            item.FotoEvolucionDescripcion = itemBase.FotoEvolucionDescripcion ?? "";
            item.FotoEvolucionId = itemBase.FotoEvolucionId;
            item.EvolucionId = itemBase.EvolucionId ?? 0;
            item.FotoEvolucionImagen = Encoding.UTF8.GetString(itemBase.FotoEvolucionImagen ?? tmp.ToArray());
            return item;
        }

        // PUT: api/FotosEvoluciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotosEvolucion(int id, EntFotoEvolucion fotosEvolucionClass)
        {

            FotosEvolucion fotosEvolucion = new FotosEvolucion();
            fotosEvolucion.FotoEvolucionDescripcion = fotosEvolucionClass.FotoEvolucionDescripcion;
            fotosEvolucion.FotoEvolucionId = fotosEvolucionClass.FotoEvolucionId;
            fotosEvolucion.EvolucionId = fotosEvolucionClass.EvolucionId;
            fotosEvolucion.FotoEvolucionImagen = Encoding.ASCII.GetBytes(fotosEvolucionClass.FotoEvolucionImagen);


            if (id != fotosEvolucion.FotoEvolucionId)
            {
                return BadRequest();
            }

            _context.Entry(fotosEvolucion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotosEvolucionExists(id))
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

        // POST: api/FotosEvoluciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public dynamic PostFotosEvolucion(EntFotoEvolucion fotosEvolucionClass)
        {
            FotosEvolucion fotosEvolucion = new FotosEvolucion();
            fotosEvolucion.FotoEvolucionDescripcion = fotosEvolucionClass.FotoEvolucionDescripcion;
            fotosEvolucion.FotoEvolucionId = fotosEvolucionClass.FotoEvolucionId;
            fotosEvolucion.EvolucionId = fotosEvolucionClass.EvolucionId;
            fotosEvolucion.FotoEvolucionImagen = Encoding.ASCII.GetBytes(fotosEvolucionClass.FotoEvolucionImagen);

          if (_context.FotosEvolucions == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.FotosEvolucions'  is null.");
          }
            _context.FotosEvolucions.Add(fotosEvolucion);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FotosEvolucionExists(fotosEvolucion.FotoEvolucionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFotosEvolucion", new { id = fotosEvolucion.FotoEvolucionId }, fotosEvolucion);
        }

        // DELETE: api/FotosEvoluciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFotosEvolucion(int id)
        {
            if (_context.FotosEvolucions == null)
            {
                return NotFound();
            }
            var fotosEvolucion = await _context.FotosEvolucions.FindAsync(id);
            if (fotosEvolucion == null)
            {
                return NotFound();
            }

            _context.FotosEvolucions.Remove(fotosEvolucion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotosEvolucionExists(int id)
        {
            return (_context.FotosEvolucions?.Any(e => e.FotoEvolucionId == id)).GetValueOrDefault();
        }
    }
}
