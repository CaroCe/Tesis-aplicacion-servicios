using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FisioFlores.Models;
using FisioFlores.Entidades;

namespace FisioFlores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosEspecialistasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public HorariosEspecialistasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/HorariosEspecialistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioEspecialistum>>> GetHorarioEspecialista()
        {
          if (_context.HorarioEspecialista == null)
          {
              return NotFound();
          }
            return await _context.HorarioEspecialista.ToListAsync();
        }

        // GET: api/HorariosEspecialistas/5
        [HttpGet("PorUsuario/{id}")]
        public  dynamic GetHorarioEspecialistum(int id)
        {
            if (_context.HorarioEspecialista == null)
            {
                return NotFound();
            }
            List<EntHorarioEspecialista> lista = new List<EntHorarioEspecialista>();
            List<HorarioEspecialistum> horariosEspecialista = new List<HorarioEspecialistum>();
            horariosEspecialista =  _context.HorarioEspecialista
                .Include(c=>c.HorarioTrabajo)
                .Where(e=>e.EspecialistaId == id)
                .ToList();
            List<HorarioDia> dias = new List<HorarioDia>();
            dias = _context.HorarioDias.ToList();
            for (int i=0;i<dias.Count;i++)
            {
                EntHorarioEspecialista item = new EntHorarioEspecialista();
                item.HorarioDiaNombre = dias[i].HorarioNombre??"";
                item.HorarioDiaId = dias[i].HorarioId;
                List<HorarioEspecialistum> trabajos = new List<HorarioEspecialistum>();
                trabajos = horariosEspecialista.Where(c=>c.HorarioId == item.HorarioDiaId).ToList();
                for (int j=0;j<trabajos.Count;j++)
                {
                    item.HorarioTrabajo.Add(new Entidades.HorarioTrabajo 
                    { 
                        Id = trabajos[j].HorarioTrabajo?.HorarioTrabajoId ?? 0,
                        HoraDesde = trabajos[j].HorarioTrabajo?.HorarioTrabajoDesde ?? "",
                        HoraHasta = trabajos[j].HorarioTrabajo?.HorarioTrabajoHasta ?? ""
                    });
                }
                lista.Add(item);
            }
            return lista;
        }

        // PUT: api/HorariosEspecialistas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioEspecialistum(int id, HorarioEspecialistum horarioEspecialistum)
        {
            if (id != horarioEspecialistum.HorarioEspecialistaId)
            {
                return BadRequest();
            }

            _context.Entry(horarioEspecialistum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioEspecialistumExists(id))
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

        // POST: api/HorariosEspecialistas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HorarioEspecialistum>> PostHorarioEspecialistum(HorarioEspecialistum horarioEspecialistum)
        {
          if (_context.HorarioEspecialista == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.HorarioEspecialista'  is null.");
          }
            _context.HorarioEspecialista.Add(horarioEspecialistum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HorarioEspecialistumExists(horarioEspecialistum.HorarioEspecialistaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHorarioEspecialistum", new { id = horarioEspecialistum.HorarioEspecialistaId }, horarioEspecialistum);
        }

        // DELETE: api/HorariosEspecialistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioEspecialistum(int id)
        {
            if (_context.HorarioEspecialista == null)
            {
                return NotFound();
            }
            var horarioEspecialistum = await _context.HorarioEspecialista.FindAsync(id);
            if (horarioEspecialistum == null)
            {
                return NotFound();
            }

            _context.HorarioEspecialista.Remove(horarioEspecialistum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioEspecialistumExists(int id)
        {
            return (_context.HorarioEspecialista?.Any(e => e.HorarioEspecialistaId == id)).GetValueOrDefault();
        }
    }
}
