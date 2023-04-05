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
    public class EjercicioTratamientosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public EjercicioTratamientosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/EjercicioTratamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EjercicioTratamiento>>> GetEjercicioTratamientos()
        {
          if (_context.EjercicioTratamientos == null)
          {
              return NotFound();
          }
            return await _context.EjercicioTratamientos.ToListAsync();
        }

        // GET: api/EjercicioTratamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EjercicioTratamiento>> GetEjercicioTratamiento(int id)
        {
          if (_context.EjercicioTratamientos == null)
          {
              return NotFound();
          }
            var ejercicioTratamiento = await _context.EjercicioTratamientos.FindAsync(id);

            if (ejercicioTratamiento == null)
            {
                return NotFound();
            }

            return ejercicioTratamiento;
        }

        // PUT: api/EjercicioTratamientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjercicioTratamiento(int id, EjercicioTratamiento ejercicioTratamiento)
        {
            if (id != ejercicioTratamiento.EjercicioTratamientoId)
            {
                return BadRequest();
            }

            _context.Entry(ejercicioTratamiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjercicioTratamientoExists(id))
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

        // POST: api/EjercicioTratamientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EjercicioTratamiento>> PostEjercicioTratamiento(EjercicioTratamiento ejercicioTratamiento)
        {
          if (_context.EjercicioTratamientos == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.EjercicioTratamientos'  is null.");
          }
            _context.EjercicioTratamientos.Add(ejercicioTratamiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EjercicioTratamientoExists(ejercicioTratamiento.EjercicioTratamientoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEjercicioTratamiento", new { id = ejercicioTratamiento.EjercicioTratamientoId }, ejercicioTratamiento);
        }

        // DELETE: api/EjercicioTratamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjercicioTratamiento(int id)
        {
            if (_context.EjercicioTratamientos == null)
            {
                return NotFound();
            }
            var ejercicioTratamiento = await _context.EjercicioTratamientos.FindAsync(id);
            if (ejercicioTratamiento == null)
            {
                return NotFound();
            }

            _context.EjercicioTratamientos.Remove(ejercicioTratamiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EjercicioTratamientoExists(int id)
        {
            return (_context.EjercicioTratamientos?.Any(e => e.EjercicioTratamientoId == id)).GetValueOrDefault();
        }
    }
}
