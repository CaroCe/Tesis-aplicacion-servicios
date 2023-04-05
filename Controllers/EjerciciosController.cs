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
    public class EjerciciosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public EjerciciosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Ejercicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ejercicio>>> GetEjercicios()
        {
          if (_context.Ejercicios == null)
          {
              return NotFound();
          }
            return await _context.Ejercicios.ToListAsync();
        }

        // GET: api/Ejercicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ejercicio>> GetEjercicio(int id)
        {
          if (_context.Ejercicios == null)
          {
              return NotFound();
          }
            var ejercicio = await _context.Ejercicios.FindAsync(id);

            if (ejercicio == null)
            {
                return NotFound();
            }

            return ejercicio;
        }

        // PUT: api/Ejercicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjercicio(int id, Ejercicio ejercicio)
        {
            if (id != ejercicio.EjercicioId)
            {
                return BadRequest();
            }

            _context.Entry(ejercicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjercicioExists(id))
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

        // POST: api/Ejercicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ejercicio>> PostEjercicio(Ejercicio ejercicio)
        {
          if (_context.Ejercicios == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Ejercicios'  is null.");
          }
            _context.Ejercicios.Add(ejercicio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EjercicioExists(ejercicio.EjercicioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEjercicio", new { id = ejercicio.EjercicioId }, ejercicio);
        }

        // DELETE: api/Ejercicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjercicio(int id)
        {
            if (_context.Ejercicios == null)
            {
                return NotFound();
            }
            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            _context.Ejercicios.Remove(ejercicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EjercicioExists(int id)
        {
            return (_context.Ejercicios?.Any(e => e.EjercicioId == id)).GetValueOrDefault();
        }
    }
}
