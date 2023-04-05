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
    public class TratamientosController : ControllerBase
    {
        private readonly bdd_fisio_floresContext _context;

        public TratamientosController(bdd_fisio_floresContext context)
        {
            _context = context;
        }

        // GET: api/Tratamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratamiento>>> GetTratamientos()
        {
          if (_context.Tratamientos == null)
          {
              return NotFound();
          }
            return await _context.Tratamientos.ToListAsync();
        }

        // GET: api/Tratamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamiento>> GetTratamiento(int id)
        {
          if (_context.Tratamientos == null)
          {
              return NotFound();
          }
            var tratamiento = await _context.Tratamientos.FindAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            return tratamiento;
        }
        [HttpGet("PorPaciente/{id}")]
        public dynamic GetTratamientoPorPaciente(int id)
        {
            List<Tratamiento> lista = new List<Tratamiento>();
            List<EntTratamiento> retorno = new List<EntTratamiento>();
            List<Consultum> consultas = new List<Consultum> ();
            List<HistoriaClinica> historias = new List<HistoriaClinica>();
            historias = _context.HistoriaClinicas.Where(h => h.PacienteId == id).ToList();

            historias.ForEach(c =>
            {
                consultas.AddRange(_context.Consulta.Where(cons => cons.HistoriaId == c.HistoriaId).ToList());
            });


            consultas.ForEach(itemCons =>
            {
                lista.AddRange(_context.Tratamientos.Where(t => t.ConsultaId == itemCons.ConsultaId).ToList());
            });

            

            lista.ForEach(t =>
            {
                EntTratamiento item = new EntTratamiento
                {
                    TratamientoId = t.TratamientoId,
                    ConsultaId = t.ConsultaId ?? 0,
                    TratamientoFechaCreacion = t.TratamientoFechaCreacion ?? DateTime.Now,
                    TratamientoDias = t.TratamientoDias ?? 0,
                    TratamientoFechaInicio = t.TratamientoFechaInicio ?? DateTime.Now,
                    TratamientoObservacion = t.TratamientoObservacion ?? "",
                    TratamientoDescripcion = t.TratamientoDescripcion ?? "",
                    TratamientoRecomendacion = t.TratamientoRecomendacion ?? "",
                    TratamientoFase = t.TratamientoFase ?? "",
                    TratamientoCompleto = t.TratamientoCompleto ?? false
                };

                List<TratamientoDium> listaDias = new List<TratamientoDium>();
                listaDias = _context.TratamientoDia.Where(td => td.TratamientoId == t.TratamientoId).ToList();
                listaDias.ForEach(ld =>
                {
                    TratamientoDia itemDia = new TratamientoDia
                    {
                        TratamientoDiaId = ld.TratamientoDiaId,
                        TratamientoId = ld.TratamientoId ?? 0,
                        TratamientoDiaFecha = ld.TratamientoDiaFecha ?? DateTime.Now
                    };
                    List<EjercicioTratamiento> listaEjercicios = new List<EjercicioTratamiento>();
                    listaEjercicios = _context.EjercicioTratamientos.Where(et => et.TratamientoDiaId == ld.TratamientoDiaId).ToList();
                    listaEjercicios.ForEach(el =>
                    {
                        EjercicioTratamientos ejerItem = new EjercicioTratamientos
                        {
                            EjercicioTratamientoId = el.EjercicioTratamientoId,
                            TratamientoDiaId = el.TratamientoDiaId ?? 0,
                            EjercicioId = el.EjercicioId ?? 0,
                            EjercicioTratamientoRepeticiones = el.EjercicioTratamientoRepeticiones ?? 0,
                            EjercicioTratamientoSerie = el.EjercicioTratamientoSerie ?? 0,
                            EjercicioEstado = el.EjercicioEstado ?? false,
                            EjercicioDescanso = el.EjercicioDescanso ?? "",
                            EjercicioObservacion = el.EjercicioObservacion ?? ""
                        };

                        Models.Ejercicio itemEjercicio = new Models.Ejercicio();
                        itemEjercicio = _context.Ejercicios.Find(el.EjercicioId) ?? new Models.Ejercicio();

                        Entidades.Ejercicio itEje = new Entidades.Ejercicio();
                        itEje.EjercicioId = itemEjercicio.EjercicioId;
                        itEje.EjercicioNombre = itemEjercicio.EjercicioNombre ?? "";
                        itEje.EjercicioGrafico = itemEjercicio.EjercicioGrafico ?? "";
                        itEje.EjercicioDescripcion = itemEjercicio.EjercicioDescripcion ?? "";
                        itEje.EjercicioEstado = itemEjercicio.EjercicioEstado ?? false;

                        ejerItem.Ejercicio = itEje;


                        itemDia.EjercicioTratamientos.Add(ejerItem);

                    });
                    item.TratamientosDia.Add(itemDia);
                });
                retorno.Add(item);

            });

            return retorno;
        }
        [HttpGet("PorConsulta/{id}")]
        public dynamic GetTratamientoPorConsulta(int id)
        {
            List<Tratamiento> lista = new List<Tratamiento>();
            List<EntTratamiento> retorno = new List<EntTratamiento>();
            lista = _context.Tratamientos.Where(t => t.ConsultaId == id).ToList();

            lista.ForEach(t =>
            {
                EntTratamiento item = new EntTratamiento
                {
                    TratamientoId = t.TratamientoId,
                    ConsultaId = t.ConsultaId??0,
                    TratamientoFechaCreacion = t.TratamientoFechaCreacion??DateTime.Now,
                    TratamientoDias = t.TratamientoDias??0,
                    TratamientoFechaInicio = t.TratamientoFechaInicio??DateTime.Now,
                    TratamientoObservacion = t.TratamientoObservacion??"",
                    TratamientoDescripcion = t.TratamientoDescripcion ?? "",
                    TratamientoRecomendacion = t.TratamientoRecomendacion ?? "",
                    TratamientoFase = t.TratamientoFase ?? "",
                    TratamientoCompleto = t.TratamientoCompleto?? false
                };

                List<TratamientoDium> listaDias = new List<TratamientoDium>();
                listaDias = _context.TratamientoDia.Where(td=>td.TratamientoId == t.TratamientoId).ToList();
                listaDias.ForEach(ld =>
                {
                    TratamientoDia itemDia = new TratamientoDia
                    {
                        TratamientoDiaId = ld.TratamientoDiaId,
                        TratamientoId = ld.TratamientoId??0,
                        TratamientoDiaFecha = ld.TratamientoDiaFecha??DateTime.Now
                    };
                    List<EjercicioTratamiento> listaEjercicios = new List<EjercicioTratamiento>();
                    listaEjercicios = _context.EjercicioTratamientos.Where(et => et.TratamientoDiaId == ld.TratamientoDiaId).ToList();
                    listaEjercicios.ForEach(el =>
                    {
                        EjercicioTratamientos ejerItem = new EjercicioTratamientos
                        {
                            EjercicioTratamientoId = el.EjercicioTratamientoId,
                            TratamientoDiaId = el.TratamientoDiaId??0,
                            EjercicioId = el.EjercicioId??0,
                            EjercicioTratamientoRepeticiones = el.EjercicioTratamientoRepeticiones??0,
                            EjercicioTratamientoSerie = el.EjercicioTratamientoSerie ?? 0,
                            EjercicioEstado = el.EjercicioEstado??false,
                            EjercicioDescanso = el.EjercicioDescanso??"",
                            EjercicioObservacion = el.EjercicioObservacion??""
                        };

                        Models.Ejercicio itemEjercicio = new Models.Ejercicio();
                        itemEjercicio = _context.Ejercicios.Find(el.EjercicioId)??new Models.Ejercicio();

                        Entidades.Ejercicio itEje = new Entidades.Ejercicio();
                        itEje.EjercicioId = itemEjercicio.EjercicioId;
                        itEje.EjercicioNombre = itemEjercicio.EjercicioNombre??"";
                        itEje.EjercicioGrafico = itemEjercicio.EjercicioGrafico??"";
                        itEje.EjercicioDescripcion = itemEjercicio.EjercicioDescripcion??"";
                        itEje.EjercicioEstado = itemEjercicio.EjercicioEstado ?? false;

                        ejerItem.Ejercicio = itEje;


                        itemDia.EjercicioTratamientos.Add(ejerItem);

                    });
                    item.TratamientosDia.Add(itemDia);
                });
                retorno.Add(item);

            });

            return retorno;
        }
        // PUT: api/Tratamientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamiento(int id, Tratamiento tratamiento)
        {
            if (id != tratamiento.TratamientoId)
            {
                return BadRequest();
            }

            _context.Entry(tratamiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientoExists(id))
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

        // POST: api/Tratamientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tratamiento>> PostTratamiento(Tratamiento tratamiento)
        {
          if (_context.Tratamientos == null)
          {
              return Problem("Entity set 'bdd_fisio_floresContext.Tratamientos'  is null.");
          }
            _context.Tratamientos.Add(tratamiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TratamientoExists(tratamiento.TratamientoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTratamiento", new { id = tratamiento.TratamientoId }, tratamiento);
        }

        // DELETE: api/Tratamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamiento(int id)
        {
            if (_context.Tratamientos == null)
            {
                return NotFound();
            }
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            _context.Tratamientos.Remove(tratamiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TratamientoExists(int id)
        {
            return (_context.Tratamientos?.Any(e => e.TratamientoId == id)).GetValueOrDefault();
        }
    }
}
