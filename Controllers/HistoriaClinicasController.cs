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
    public class HistoriaClinicasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public HistoriaClinicasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/HistoriaClinicas
        [HttpGet]
        public dynamic GetHistoriaClinicas()
        {
            List<EntUsuarioHistoria> listaRetorno = new List<EntUsuarioHistoria>();
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            List<Usuario> lista = new List<Usuario>();
            lista = _context.Usuarios.ToList();


            lista.ForEach(c =>
            {
                HistoriaClinica itemH = new HistoriaClinica();
                try
                {
                    itemH = _context.HistoriaClinicas.Where(x => x.PacienteId == c.UsuarioId).ToList()[0];
                }
                catch (Exception)
                {
                    itemH.HistoriaId = 0;
                }
                if(itemH.HistoriaId != 0)
                {
                    EntUsuarioHistoria item = new EntUsuarioHistoria
                    {
                        LateralidadId = c.LateralidadId ?? 0,
                        UsuarioProfesion = c.UsuarioProfesion ?? "",
                        UsuarioOcupacion = c.UsuarioOcupacion ?? "",
                        RolId = c.RolId ?? 0,
                        SedeId = c.SedeId ?? 0,
                        UsuarioCorreo = c.UsuarioCorreo ?? "",
                        UsuarioDireccion = c.UsuarioDireccion ?? "",
                        UsuarioFechaNacimiento = c.UsuarioFechaNacimiento ?? DateTime.Now,
                        UsuarioId = c.UsuarioId,
                        UsuarioIdentificacion = c.UsuarioIdentificacion ?? "",
                        UsuarioNombre = c.UsuarioNombre ?? "",
                        UsuarioTelefono = c.UsuarioTelefono ?? "",
                        UsuarioEstado = c.UsuarioEstado ?? false,
                        HistoriaId = itemH.HistoriaId,
                        EspecialistaId = itemH.EspecialistaId ?? 0,
                        HistoriaActFisica = itemH.HistoriaActFisica ?? "",
                        HistoriaAlergias = itemH.HistoriaAlergias ?? "",
                        HistoriaAntecedentes = itemH.HistoriaAntecedentes ?? "",
                        HistoriaFecha = itemH.HistoriaFecha ?? DateTime.Now,
                        HistoriaFuente = itemH.HistoriaFuente ?? "",
                        HistoriaHabitos = itemH.HistoriaHabitos ?? "",
                        HistoriaPatologicos = itemH.HistoriaPatologicos ?? "",
                        HistoriaVivienda = itemH.HistoriaVivienda ?? "",
                        PacienteId = itemH.PacienteId ?? 0
                    };

                    listaRetorno.Add(item);
                }
                
            });

            return listaRetorno;
        }
        [HttpPost("Filtros")]
        public dynamic PostFiltrosHistoriaClinicas(FiltroConsulta filtro)
        {
            List<EntUsuarioHistoria> listaRetorno = new List<EntUsuarioHistoria>();
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            List<Usuario> lista = new List<Usuario>();
            lista = _context.Usuarios.ToList();

            List<Lateralidad> lateralidades = new List<Lateralidad>();
            lateralidades = _context.Lateralidads.ToList();
            lista.ForEach(c =>
            {
                HistoriaClinica itemH = new HistoriaClinica();
                try
                {
                    itemH = _context.HistoriaClinicas.Where(x => x.PacienteId == c.UsuarioId).ToList()[0];
                }
                catch (Exception)
                {
                    itemH.HistoriaId = 0;
                }
                if (itemH.HistoriaId != 0)
                {
                    EntUsuarioHistoria item = new EntUsuarioHistoria
                    {
                        LateralidadId = c.LateralidadId ?? 0,
                        LateralidadNombre = lateralidades.Find(k=>k.LateralidadId == c.LateralidadId)?.LateralidadNombre??"",
                        UsuarioProfesion = c.UsuarioProfesion ?? "",
                        UsuarioOcupacion = c.UsuarioOcupacion ?? "",
                        RolId = c.RolId ?? 0,
                        SedeId = c.SedeId ?? 0,
                        UsuarioCorreo = c.UsuarioCorreo ?? "",
                        UsuarioDireccion = c.UsuarioDireccion ?? "",
                        UsuarioFechaNacimiento = c.UsuarioFechaNacimiento ?? DateTime.Now,
                        UsuarioId = c.UsuarioId,
                        UsuarioIdentificacion = c.UsuarioIdentificacion ?? "",
                        UsuarioNombre = c.UsuarioNombre ?? "",
                        UsuarioTelefono = c.UsuarioTelefono ?? "",
                        UsuarioEstado = c.UsuarioEstado ?? false,
                        HistoriaId = itemH.HistoriaId,
                        EspecialistaId = itemH.EspecialistaId ?? 0,
                        HistoriaActFisica = itemH.HistoriaActFisica ?? "",
                        HistoriaAlergias = itemH.HistoriaAlergias ?? "",
                        HistoriaAntecedentes = itemH.HistoriaAntecedentes ?? "",
                        HistoriaFecha = itemH.HistoriaFecha ?? DateTime.Now,
                        HistoriaFuente = itemH.HistoriaFuente ?? "",
                        HistoriaHabitos = itemH.HistoriaHabitos ?? "",
                        HistoriaPatologicos = itemH.HistoriaPatologicos ?? "",
                        HistoriaVivienda = itemH.HistoriaVivienda ?? "",
                        PacienteId = itemH.PacienteId ?? 0
                    };

                    listaRetorno.Add(item);
                }

            });
            if (filtro.Cedula.Length > 0)
            {
                listaRetorno = listaRetorno.Where(c => c.UsuarioIdentificacion.Contains(filtro.Cedula)).ToList();
            }
            if (filtro.PacienteId > 0)
            {
                listaRetorno = listaRetorno.Where(c => c.UsuarioId == filtro.PacienteId).ToList();
            }
            return listaRetorno.Where(c => c.HistoriaFecha >= filtro.FechaDesde && c.HistoriaFecha <= filtro.FechaHasta);
        }
        // GET: api/HistoriaClinicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriaClinica>> GetHistoriaClinica(int id)
        {
          if (_context.HistoriaClinicas == null)
          {
              return NotFound();
          }
            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);

            if (historiaClinica == null)
            {
                return NotFound();
            }

            return historiaClinica;
        }

        // PUT: api/HistoriaClinicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoriaClinica(int id, HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.HistoriaId)
            {
                return BadRequest();
            }

            _context.Entry(historiaClinica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoriaClinicaExists(id))
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

        // POST: api/HistoriaClinicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoriaClinica>> PostHistoriaClinica(HistoriaClinica historiaClinica)
        {
          if (_context.HistoriaClinicas == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.HistoriaClinicas'  is null.");
          }
            _context.HistoriaClinicas.Add(historiaClinica);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistoriaClinicaExists(historiaClinica.HistoriaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHistoriaClinica", new { id = historiaClinica.HistoriaId }, historiaClinica);
        }

        // DELETE: api/HistoriaClinicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoriaClinica(int id)
        {
            if (_context.HistoriaClinicas == null)
            {
                return NotFound();
            }
            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            _context.HistoriaClinicas.Remove(historiaClinica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoriaClinicaExists(int id)
        {
            return (_context.HistoriaClinicas?.Any(e => e.HistoriaId == id)).GetValueOrDefault();
        }
    }
}
