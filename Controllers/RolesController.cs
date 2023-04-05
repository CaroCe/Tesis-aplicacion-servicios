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
    public class RolesController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public RolesController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRols()
        {
          if (_context.Rols == null)
          {
              return NotFound();
          }
            return await _context.Rols.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
          if (_context.Rols == null)
          {
              return NotFound();
          }
            var rol = await _context.Rols.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol rol)
        {
            if (id != rol.RolId)
            {
                return BadRequest();
            }

            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
          if (_context.Rols == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Rols'  is null.");
          }
            _context.Rols.Add(rol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RolExists(rol.RolId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRol", new { id = rol.RolId }, rol);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            if (_context.Rols == null)
            {
                return NotFound();
            }
            var rol = await _context.Rols.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return (_context.Rols?.Any(e => e.RolId == id)).GetValueOrDefault();
        }
    }
}
