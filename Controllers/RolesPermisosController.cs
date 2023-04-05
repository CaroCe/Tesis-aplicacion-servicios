using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FisioFlores.Models;
using SagatFarmServices.Entidades;

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesPermisosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public RolesPermisosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/RolesPermisos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolPermiso>>> GetRolPermisos()
        {
          if (_context.RolPermisos == null)
          {
              return NotFound();
          }
            return await _context.RolPermisos.ToListAsync();
        }

        // GET: api/RolesPermisos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolPermiso>> GetRolPermiso(int id)
        {
          if (_context.RolPermisos == null)
          {
              return NotFound();
          }
            var rolPermiso = await _context.RolPermisos.FindAsync(id);

            if (rolPermiso == null)
            {
                return NotFound();
            }

            return rolPermiso;
        }

        // PUT: api/RolesPermisos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolPermiso(int id, RolPermiso rolPermiso)
        {
            if (id != rolPermiso.RolPermisoId)
            {
                return BadRequest();
            }

            _context.Entry(rolPermiso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolPermisoExists(id))
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

        // POST: api/RolesPermisos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RolPermiso>> PostRolPermiso(RolPermiso rolPermiso)
        {


            if (_context.RolPermisos == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.RolPermisos'  is null.");
          }
            _context.RolPermisos.Add(rolPermiso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RolPermisoExists(rolPermiso.RolPermisoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRolPermiso", new { id = rolPermiso.RolPermisoId }, rolPermiso);
        }

        // DELETE: api/RolesPermisos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolPermiso(int id)
        {
            if (_context.RolPermisos == null)
            {
                return NotFound();
            }
            var rolPermiso = await _context.RolPermisos.FindAsync(id);
            if (rolPermiso == null)
            {
                return NotFound();
            }

            _context.RolPermisos.Remove(rolPermiso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolPermisoExists(int id)
        {
            return (_context.RolPermisos?.Any(e => e.RolPermisoId == id)).GetValueOrDefault();
        }
    }
}
