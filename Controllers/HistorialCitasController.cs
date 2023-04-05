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
    public class HistorialCitasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public HistorialCitasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/HistorialCitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCitum>>> GetHistorialCita()
        {
          if (_context.HistorialCita == null)
          {
              return NotFound();
          }
            return await _context.HistorialCita.ToListAsync();
        }

        // GET: api/HistorialCitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialCitum>> GetHistorialCitum(int id)
        {
          if (_context.HistorialCita == null)
          {
              return NotFound();
          }
            var historialCitum = await _context.HistorialCita.FindAsync(id);

            if (historialCitum == null)
            {
                return NotFound();
            }

            return historialCitum;
        }

        // PUT: api/HistorialCitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialCitum(int id, HistorialCitum historialCitum)
        {
            if (id != historialCitum.HistorialCitaId)
            {
                return BadRequest();
            }

            _context.Entry(historialCitum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialCitumExists(id))
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

        // POST: api/HistorialCitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialCitum>> PostHistorialCitum(HistorialCitum historialCitum)
        {
          if (_context.HistorialCita == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.HistorialCita'  is null.");
          }
            _context.HistorialCita.Add(historialCitum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistorialCitumExists(historialCitum.HistorialCitaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHistorialCitum", new { id = historialCitum.HistorialCitaId }, historialCitum);
        }

        // DELETE: api/HistorialCitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialCitum(int id)
        {
            if (_context.HistorialCita == null)
            {
                return NotFound();
            }
            var historialCitum = await _context.HistorialCita.FindAsync(id);
            if (historialCitum == null)
            {
                return NotFound();
            }

            _context.HistorialCita.Remove(historialCitum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialCitumExists(int id)
        {
            return (_context.HistorialCita?.Any(e => e.HistorialCitaId == id)).GetValueOrDefault();
        }
    }
}
