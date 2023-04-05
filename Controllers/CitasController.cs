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
    public class CitasController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public CitasController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citum>>> GetCita()
        {
          if (_context.Cita == null)
          {
              return NotFound();
          }
            return await _context.Cita.ToListAsync();
        }

        [HttpPost("Admin")]
        public dynamic GetCitaAdmin(Filtro filtro)
        {
            if (_context.Cita == null)
            {
                return NotFound();
            }

            List<EntAdminCita> listaCitas = new List<EntAdminCita>();
            List<Citum> listaCitasBase = _context.Cita.Include(k=>k.Especialista).Include(k=>k.Usuario)
                .Where(c=>(c.UsuarioId == filtro.PacienteId || filtro.PacienteId == 0) && (c.EspecialistaId == filtro.EspecialistaId || filtro.EspecialistaId == 0) && (c.CitaEstado == filtro.Estado || filtro.Estado == 5)).ToList();


            listaCitasBase.ForEach(c =>
            {
                EntAdminCita item = new EntAdminCita();
                item.Especialista = c.Especialista?.UsuarioNombre??"";
                item.Paciente = c.Usuario?.UsuarioNombre ?? "";
                item.PacienteId = c.Usuario?.UsuarioId??0;
                item.Id = c.CitaId;
                item.Hora = c.CitaHora??"";
                item.Observacion = c.CitaObservacion??"";
                item.EstadoId = c.CitaEstado??0;
                item.Estado = item.EstadoId == 0 ? "Finalizada" : item.EstadoId == 1 ? "Por Confirmar" : item.EstadoId == 2 ? "Cancelada" : item.EstadoId == 3 ? "Confirmado" : "";
                item.Fecha = c.CitaFecha?.ToShortDateString()??"";
                DateTime fecha = new DateTime(c.CitaFecha?.Year??2022, c.CitaFecha?.Month??1, c.CitaFecha?.Day??1,0,0,0);
                DateTime fechaFiltroDesde = new DateTime(filtro.FechaDesde?.Year ?? 2022, filtro.FechaDesde?.Month ?? 1, filtro.FechaDesde?.Day ?? 1, 0, 0, 0);
                DateTime fechaFiltroHasta = new DateTime(filtro.FechaHasta?.Year ?? 2022, filtro.FechaHasta?.Month ?? 1, filtro.FechaHasta?.Day ?? 1, 0, 0, 0);
                if (fecha >= fechaFiltroDesde && fecha <= fechaFiltroHasta)
                {
                    listaCitas.Add(item);
                }
            });

            return listaCitas;
        }
        [HttpGet("Estado/{id}/{estado}")]
        public dynamic GetCambiarEstado(int id, int estado)
        {

            Citum citum = new Citum();

            citum = _context.Cita.First(c => c.CitaId == id);
            citum.CitaEstado = estado;
            if (id != citum.CitaId)
            {
                return BadRequest();
            }

            _context.Entry(citum).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitumExists(id))
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

        [HttpPost("HorarioDisponible")]
        public dynamic PostHorariosDisponibles(EntFiltro filtro)
        {
            DateTime fecha = new DateTime();
            fecha = filtro.fechaDesde;
            List<HorarioEspecialistum> horariosEspecialista = new List<HorarioEspecialistum>();
            List<Usuario> listaUsuarios = new List<Usuario>();
            
            
            if (filtro.sedeId == 0 && filtro.especialistaId != 0)
            {
                horariosEspecialista = _context.HorarioEspecialista
                .Include(c => c.HorarioTrabajo)
                .Where(e => e.EspecialistaId == filtro.especialistaId)
                .ToList();
            }else if (filtro.sedeId != 0 && filtro.especialistaId == 0)
            {
                listaUsuarios = _context.Usuarios.Where(s => s.SedeId == filtro.sedeId).ToList();
                listaUsuarios.ForEach(c =>
                {
                    horariosEspecialista.AddRange(_context.HorarioEspecialista
                        .Include(c => c.HorarioTrabajo)
                        .Where(e => e.EspecialistaId == c.UsuarioId)
                        .ToList());
                });
            }
            else if(filtro.sedeId != 0 && filtro.especialistaId != 0 )
            {
                listaUsuarios = _context.Usuarios.Where(s => s.SedeId == filtro.sedeId).ToList();
                listaUsuarios.ForEach(c =>
                {
                    if (c.UsuarioId == filtro.especialistaId)
                    {
                        horariosEspecialista.AddRange(_context.HorarioEspecialista
                        .Include(c => c.HorarioTrabajo)
                        .Where(e => e.EspecialistaId == c.UsuarioId)
                        .ToList());
                    }
                });
            }
            else
            {
                horariosEspecialista = _context.HorarioEspecialista
                .Include(c => c.HorarioTrabajo)
                .ToList();
            }
            
 

            List<Citum> listaCitasConsulta = new List<Citum>();
            List<EntCita> listaCitas = new List<EntCita>();
            listaCitasConsulta = _context.Cita.ToList();
            listaCitasConsulta.ForEach(c =>
            {
                EntCita itemCita = new EntCita();
                itemCita.CitaHora = c.CitaHora??"";
                itemCita.CitaFecha = c.CitaFecha??new DateTime();
                itemCita.CitaEstado = c.CitaEstado??0;
                itemCita.CitaId = c.CitaId;
                if (itemCita.CitaEstado != 2 && itemCita.CitaEstado != 0)
                {
                    listaCitas.Add(itemCita);
                }
            });

            List<HorarioDia> dias = new List<HorarioDia>();
            dias = _context.HorarioDias.ToList();
            List<EntHorarioCita> retorno = new List<EntHorarioCita>();
            for (DateTime fechaTmp = fecha;fechaTmp <= filtro.fechaHasta; fechaTmp = fechaTmp.AddDays(1))
            {

                List<EntCita> listaCitasTmp = new List<EntCita>();
                listaCitasTmp = listaCitas.Where(c => c.CitaFecha.Year == fechaTmp.Year && c.CitaFecha.Month == fechaTmp.Month && c.CitaFecha.Day == fechaTmp.Day).ToList();
                int i = (int)fechaTmp.DayOfWeek == 0 ? 7 : (int)fechaTmp.DayOfWeek;
                EntHorarioCita item = new EntHorarioCita();
                item.HorarioDiaId = i;
                item.HorarioDiaNombre = dias.First(c => c.HorarioId == i).HorarioNombre + " " + fechaTmp.Day;
                item.HorarioDiaFecha = fechaTmp;
                List<HorarioEspecialistum> trabajos = new List<HorarioEspecialistum>();
                trabajos = horariosEspecialista.Where(h=>h.HorarioId == i).ToList();
                int min = 0;
                int max = 0;
                if(trabajos.Count>0){
                    min = Convert.ToInt32(trabajos[0].HorarioTrabajo?.HorarioTrabajoDesde?.Split(":")[0]);
                    max = Convert.ToInt32(trabajos[0].HorarioTrabajo?.HorarioTrabajoDesde?.Split(":")[0]);
                    List<Horas> horas = new List<Horas>();
                    for (int j = 0; j < trabajos.Count; j++)
                    {
                        for (int k = Convert.ToInt32(trabajos[j].HorarioTrabajo?.HorarioTrabajoDesde?.Split(":")[0]); k < Convert.ToInt32(trabajos[j].HorarioTrabajo?.HorarioTrabajoHasta?.Split(":")[0]); k++)
                        {
                            Horas itemHora = new Horas();
                            itemHora.hora = k;
                            itemHora.minuto = Convert.ToInt32(trabajos[j].HorarioTrabajo?.HorarioTrabajoDesde?.Split(":")[1]);
                            horas.Add(itemHora);
                        }
                    }

                    while (horas.Count > 0)
                    {
                        int horaEntero = horas[0].hora;
                        int minutoEntero = horas[0].minuto;
                        item.HorarioCitas.Add(new HorarioCita
                        {
                            Id = horas[0].hora,
                            HoraCita = horaEntero.ToString() + ":" + minutoEntero.ToString(),
                            Disponibles = horas.Count(h => h.hora == horaEntero && h.minuto == minutoEntero) - listaCitasTmp.Count(h => Convert.ToInt32(h.CitaHora?.Split(":")[0]) == horaEntero && Convert.ToInt32(h.CitaHora?.Split(":")[1]) == minutoEntero)
                        });
                        horas.RemoveAll(c => c.hora == horaEntero && c.minuto == minutoEntero);
                    }
                }
                item.HorarioCitas = item.HorarioCitas.OrderBy(c=>c.Id).ToList();
                retorno.Add(item);
            }


            return retorno;
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citum>> GetCitum(int id)
        {
          if (_context.Cita == null)
          {
              return NotFound();
          }
            var citum = await _context.Cita.FindAsync(id);

            if (citum == null)
            {
                return NotFound();
            }

            return citum;
        }

        // PUT: api/Citas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitum(int id, Citum citum)
        {
            if (id != citum.CitaId)
            {
                return BadRequest();
            }

            _context.Entry(citum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitumExists(id))
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

        // POST: api/Citas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Citum>> PostCitum(Citum citum)
        {
          if (_context.Cita == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Cita'  is null.");
          }
            _context.Cita.Add(citum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CitumExists(citum.CitaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCitum", new { id = citum.CitaId }, citum);
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitum(int id)
        {
            if (_context.Cita == null)
            {
                return NotFound();
            }
            var citum = await _context.Cita.FindAsync(id);
            if (citum == null)
            {
                return NotFound();
            }

            _context.Cita.Remove(citum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitumExists(int id)
        {
            return (_context.Cita?.Any(e => e.CitaId == id)).GetValueOrDefault();
        }
    }
}
