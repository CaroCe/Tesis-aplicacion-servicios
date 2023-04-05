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
    public class TratamientosDiasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public TratamientosDiasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/TratamientosDias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientoDium>>> GetTratamientoDia()
        {
          if (_context.TratamientoDia == null)
          {
              return NotFound();
          }
            return await _context.TratamientoDia.ToListAsync();
        }

        // GET: api/TratamientosDias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TratamientoDium>> GetTratamientoDium(int id)
        {
          if (_context.TratamientoDia == null)
          {
              return NotFound();
          }
            var tratamientoDium = await _context.TratamientoDia.FindAsync(id);

            if (tratamientoDium == null)
            {
                return NotFound();
            }

            return tratamientoDium;
        }

        // PUT: api/TratamientosDias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamientoDium(int id, TratamientoDium tratamientoDium)
        {
            if (id != tratamientoDium.TratamientoDiaId)
            {
                return BadRequest();
            }

            _context.Entry(tratamientoDium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientoDiumExists(id))
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

        // POST: api/TratamientosDias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TratamientoDium>> PostTratamientoDium(TratamientoDium tratamientoDium)
        {
          if (_context.TratamientoDia == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.TratamientoDia'  is null.");
          }
            _context.TratamientoDia.Add(tratamientoDium);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TratamientoDiumExists(tratamientoDium.TratamientoDiaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTratamientoDium", new { id = tratamientoDium.TratamientoDiaId }, tratamientoDium);
        }

        // DELETE: api/TratamientosDias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamientoDium(int id)
        {
            if (_context.TratamientoDia == null)
            {
                return NotFound();
            }
            var tratamientoDium = await _context.TratamientoDia.FindAsync(id);
            if (tratamientoDium == null)
            {
                return NotFound();
            }

            _context.TratamientoDia.Remove(tratamientoDium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TratamientoDiumExists(int id)
        {
            return (_context.TratamientoDia?.Any(e => e.TratamientoDiaId == id)).GetValueOrDefault();
        }
    }
}
