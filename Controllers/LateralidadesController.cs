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
    public class LateralidadesController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public LateralidadesController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Lateralidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lateralidad>>> GetLateralidads()
        {
          if (_context.Lateralidads == null)
          {
              return NotFound();
          }
            return await _context.Lateralidads.ToListAsync();
        }

        // GET: api/Lateralidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lateralidad>> GetLateralidad(int id)
        {
          if (_context.Lateralidads == null)
          {
              return NotFound();
          }
            var lateralidad = await _context.Lateralidads.FindAsync(id);

            if (lateralidad == null)
            {
                return NotFound();
            }

            return lateralidad;
        }

        // PUT: api/Lateralidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLateralidad(int id, Lateralidad lateralidad)
        {
            if (id != lateralidad.LateralidadId)
            {
                return BadRequest();
            }

            _context.Entry(lateralidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LateralidadExists(id))
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

        // POST: api/Lateralidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lateralidad>> PostLateralidad(Lateralidad lateralidad)
        {
          if (_context.Lateralidads == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Lateralidads'  is null.");
          }
            _context.Lateralidads.Add(lateralidad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LateralidadExists(lateralidad.LateralidadId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLateralidad", new { id = lateralidad.LateralidadId }, lateralidad);
        }

        // DELETE: api/Lateralidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLateralidad(int id)
        {
            if (_context.Lateralidads == null)
            {
                return NotFound();
            }
            var lateralidad = await _context.Lateralidads.FindAsync(id);
            if (lateralidad == null)
            {
                return NotFound();
            }

            _context.Lateralidads.Remove(lateralidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LateralidadExists(int id)
        {
            return (_context.Lateralidads?.Any(e => e.LateralidadId == id)).GetValueOrDefault();
        }
    }
}
