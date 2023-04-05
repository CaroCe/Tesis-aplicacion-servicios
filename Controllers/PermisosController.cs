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
    public class PermisosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public PermisosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Permisos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permiso>>> GetPermisos()
        {
          if (_context.Permisos == null)
          {
              return NotFound();
          }
            return await _context.Permisos.ToListAsync();
        }

        // GET: api/Permisos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permiso>> GetPermiso(int id)
        {
          if (_context.Permisos == null)
          {
              return NotFound();
          }
            var permiso = await _context.Permisos.FindAsync(id);

            if (permiso == null)
            {
                return NotFound();
            }

            return permiso;
        }

        // PUT: api/Permisos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermiso(int id, Permiso permiso)
        {
            if (id != permiso.PermisoId)
            {
                return BadRequest();
            }

            _context.Entry(permiso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisoExists(id))
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

        // POST: api/Permisos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Permiso>> PostPermiso(Permiso permiso)
        {
          if (_context.Permisos == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Permisos'  is null.");
          }
            _context.Permisos.Add(permiso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PermisoExists(permiso.PermisoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPermiso", new { id = permiso.PermisoId }, permiso);
        }

        // DELETE: api/Permisos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermiso(int id)
        {
            if (_context.Permisos == null)
            {
                return NotFound();
            }
            var permiso = await _context.Permisos.FindAsync(id);
            if (permiso == null)
            {
                return NotFound();
            }

            _context.Permisos.Remove(permiso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermisoExists(int id)
        {
            return (_context.Permisos?.Any(e => e.PermisoId == id)).GetValueOrDefault();
        }
    }
}
