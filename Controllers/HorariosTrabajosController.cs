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
    public class HorariosTrabajosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public HorariosTrabajosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/HorariosTrabajos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioTrabajo>>> GetHorarioTrabajos()
        {
          if (_context.HorarioTrabajos == null)
          {
              return NotFound();
          }
            return await _context.HorarioTrabajos.ToListAsync();
        }

        // GET: api/HorariosTrabajos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioTrabajo>> GetHorarioTrabajo(int id)
        {
          if (_context.HorarioTrabajos == null)
          {
              return NotFound();
          }
            var horarioTrabajo = await _context.HorarioTrabajos.FindAsync(id);

            if (horarioTrabajo == null)
            {
                return NotFound();
            }

            return horarioTrabajo;
        }

        // PUT: api/HorariosTrabajos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioTrabajo(int id, HorarioTrabajo horarioTrabajo)
        {
            if (id != horarioTrabajo.HorarioTrabajoId)
            {
                return BadRequest();
            }

            _context.Entry(horarioTrabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioTrabajoExists(id))
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

        // POST: api/HorariosTrabajos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HorarioTrabajo>> PostHorarioTrabajo(HorarioTrabajo horarioTrabajo)
        {
          if (_context.HorarioTrabajos == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.HorarioTrabajos'  is null.");
          }
            _context.HorarioTrabajos.Add(horarioTrabajo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HorarioTrabajoExists(horarioTrabajo.HorarioTrabajoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHorarioTrabajo", new { id = horarioTrabajo.HorarioTrabajoId }, horarioTrabajo);
        }

        // DELETE: api/HorariosTrabajos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioTrabajo(int id)
        {
            if (_context.HorarioTrabajos == null)
            {
                return NotFound();
            }
            var horarioTrabajo = await _context.HorarioTrabajos.FindAsync(id);
            if (horarioTrabajo == null)
            {
                return NotFound();
            }

            _context.HorarioTrabajos.Remove(horarioTrabajo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioTrabajoExists(int id)
        {
            return (_context.HorarioTrabajos?.Any(e => e.HorarioTrabajoId == id)).GetValueOrDefault();
        }
    }
}
