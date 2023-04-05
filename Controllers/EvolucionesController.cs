using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FisioFlores.Models;

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvolucionesController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public EvolucionesController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Evoluciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evolucion>>> GetEvolucions()
        {
          if (_context.Evolucions == null)
          {
              return NotFound();
          }
            return await _context.Evolucions.ToListAsync();
        }

        // GET: api/Evoluciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evolucion>> GetEvolucion(int id)
        {
          if (_context.Evolucions == null)
          {
              return NotFound();
          }
            var evolucion = await _context.Evolucions.FindAsync(id);

            if (evolucion == null)
            {
                return NotFound();
            }

            return evolucion;
        }
        [HttpGet("PorConsulta/{id}")]
        public dynamic GetEvolucionConsulta(int id)
        {
            if (_context.Evolucions == null)
            {
                return NotFound();
            }
            List<Evolucion> evolucion = new List<Evolucion>();
            evolucion = _context.Evolucions.Where(c=>c.ConsultaId == id).ToList();

            if (evolucion == null)
            {
                return NotFound();
            }

            return evolucion;
        }
        // PUT: api/Evoluciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvolucion(int id, Evolucion evolucion)
        {
            if (id != evolucion.EvolucionId)
            {
                return BadRequest();
            }

            _context.Entry(evolucion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvolucionExists(id))
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

        // POST: api/Evoluciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evolucion>> PostEvolucion(Evolucion evolucion)
        {
          if (_context.Evolucions == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Evolucions'  is null.");
          }
            _context.Evolucions.Add(evolucion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EvolucionExists(evolucion.EvolucionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEvolucion", new { id = evolucion.EvolucionId }, evolucion);
        }

        // DELETE: api/Evoluciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvolucion(int id)
        {
            if (_context.Evolucions == null)
            {
                return NotFound();
            }
            var evolucion = await _context.Evolucions.FindAsync(id);
            if (evolucion == null)
            {
                return NotFound();
            }

            _context.Evolucions.Remove(evolucion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvolucionExists(int id)
        {
            return (_context.Evolucions?.Any(e => e.EvolucionId == id)).GetValueOrDefault();
        }
    }
}
