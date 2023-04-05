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
    public class HorariosDiasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public HorariosDiasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/HorariosDias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDia>>> GetHorarioDias()
        {
          if (_context.HorarioDias == null)
          {
              return NotFound();
          }
            return await _context.HorarioDias.ToListAsync();
        }

        // GET: api/HorariosDias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDia>> GetHorarioDia(int id)
        {
          if (_context.HorarioDias == null)
          {
              return NotFound();
          }
            var horarioDia = await _context.HorarioDias.FindAsync(id);

            if (horarioDia == null)
            {
                return NotFound();
            }

            return horarioDia;
        }

        // PUT: api/HorariosDias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioDia(int id, HorarioDia horarioDia)
        {
            if (id != horarioDia.HorarioId)
            {
                return BadRequest();
            }

            _context.Entry(horarioDia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioDiaExists(id))
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

        // POST: api/HorariosDias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HorarioDia>> PostHorarioDia(HorarioDia horarioDia)
        {
          if (_context.HorarioDias == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.HorarioDias'  is null.");
          }
            _context.HorarioDias.Add(horarioDia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HorarioDiaExists(horarioDia.HorarioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHorarioDia", new { id = horarioDia.HorarioId }, horarioDia);
        }

        // DELETE: api/HorariosDias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioDia(int id)
        {
            if (_context.HorarioDias == null)
            {
                return NotFound();
            }
            var horarioDia = await _context.HorarioDias.FindAsync(id);
            if (horarioDia == null)
            {
                return NotFound();
            }

            _context.HorarioDias.Remove(horarioDia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioDiaExists(int id)
        {
            return (_context.HorarioDias?.Any(e => e.HorarioId == id)).GetValueOrDefault();
        }
    }
}
