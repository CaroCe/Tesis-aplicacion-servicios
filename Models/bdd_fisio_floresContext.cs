using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FisioFlores.Models
{
    public partial class bdd_fisio_floresContext : DbContext
    {
        public bdd_fisio_floresContext()
        {
        }

        public bdd_fisio_floresContext(DbContextOptions<bdd_fisio_floresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Citum> Cita { get; set; } = null!;
        public virtual DbSet<ComentarioForo> ComentarioForos { get; set; } = null!;
        public virtual DbSet<Consultum> Consulta { get; set; } = null!;
        public virtual DbSet<Ejercicio> Ejercicios { get; set; } = null!;
        public virtual DbSet<EjercicioTratamiento> EjercicioTratamientos { get; set; } = null!;
        public virtual DbSet<Evolucion> Evolucions { get; set; } = null!;
        public virtual DbSet<Foro> Foros { get; set; } = null!;
        public virtual DbSet<FotosEvolucion> FotosEvolucions { get; set; } = null!;
        public virtual DbSet<FotosExaminacion> FotosExaminacions { get; set; } = null!;
        public virtual DbSet<HistoriaClinica> HistoriaClinicas { get; set; } = null!;
        public virtual DbSet<HistorialCitum> HistorialCita { get; set; } = null!;
        public virtual DbSet<HorarioDia> HorarioDias { get; set; } = null!;
        public virtual DbSet<HorarioEspecialistum> HorarioEspecialista { get; set; } = null!;
        public virtual DbSet<HorarioTrabajo> HorarioTrabajos { get; set; } = null!;
        public virtual DbSet<Lateralidad> Lateralidads { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolPermiso> RolPermisos { get; set; } = null!;
        public virtual DbSet<Sede> Sedes { get; set; } = null!;
        public virtual DbSet<Tratamiento> Tratamientos { get; set; } = null!;
        public virtual DbSet<TratamientoDium> TratamientoDia { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Citum>(entity =>
            {
                entity.HasKey(e => e.CitaId)
                    .HasName("PRIMARY");

                entity.ToTable("cita");

                entity.HasIndex(e => e.UsuarioId, "FK_RELATIONSHIP_14");

                entity.HasIndex(e => e.EspecialistaId, "FK_RELATIONSHIP_ESPECIALISTA_CITA");

                entity.Property(e => e.CitaId).HasColumnName("CITA_ID");

                entity.Property(e => e.CitaEstado).HasColumnName("CITA_ESTADO");

                entity.Property(e => e.CitaFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("CITA_FECHA");

                entity.Property(e => e.CitaHora)
                    .HasMaxLength(10)
                    .HasColumnName("CITA_HORA");

                entity.Property(e => e.CitaObservacion)
                    .HasMaxLength(200)
                    .HasColumnName("CITA_OBSERVACION");

                entity.Property(e => e.EspecialistaId).HasColumnName("ESPECIALISTA_ID");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Especialista)
                    .WithMany(p => p.CitumEspecialista)
                    .HasForeignKey(d => d.EspecialistaId)
                    .HasConstraintName("FK_RELATIONSHIP_ESPECIALISTA_CITA");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.CitumUsuarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_RELATIONSHIP_14");
            });

            modelBuilder.Entity<ComentarioForo>(entity =>
            {
                entity.ToTable("comentario_foro");

                entity.HasIndex(e => e.ForoId, "FK_FORO_COMENTARIO_FORO");

                entity.HasIndex(e => e.UsuarioId, "FK_FORO_COMENTARIO_USUARIO_ID");

                entity.Property(e => e.ComentarioForoId).HasColumnName("COMENTARIO_FORO_ID");

                entity.Property(e => e.ComentarioForoMensaje)
                    .HasMaxLength(1000)
                    .HasColumnName("COMENTARIO_FORO_MENSAJE");

                entity.Property(e => e.ForoId).HasColumnName("FORO_ID");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Foro)
                    .WithMany(p => p.ComentarioForos)
                    .HasForeignKey(d => d.ForoId)
                    .HasConstraintName("FK_FORO_COMENTARIO_FORO");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ComentarioForos)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_FORO_COMENTARIO_USUARIO_ID");
            });

            modelBuilder.Entity<Consultum>(entity =>
            {
                entity.HasKey(e => e.ConsultaId)
                    .HasName("PRIMARY");

                entity.ToTable("consulta");

                entity.HasIndex(e => e.HistoriaId, "FK_RELATIONSHIP_1");

                entity.HasIndex(e => e.EspecialistaId, "FK_RELATIONSHIP_2");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.ConsultaDescripImagen)
                    .HasMaxLength(2000)
                    .HasColumnName("CONSULTA_DESCRIP_IMAGEN");

                entity.Property(e => e.ConsultaDescripcion)
                    .HasMaxLength(2000)
                    .HasColumnName("CONSULTA_DESCRIPCION");

                entity.Property(e => e.ConsultaFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("CONSULTA_FECHA");

                entity.Property(e => e.ConsultaImagen).HasColumnName("CONSULTA_IMAGEN");

                entity.Property(e => e.ConsultaMotivo)
                    .HasMaxLength(2000)
                    .HasColumnName("CONSULTA_MOTIVO");

                entity.Property(e => e.ConsultaProblema)
                    .HasMaxLength(2000)
                    .HasColumnName("CONSULTA_PROBLEMA");

                entity.Property(e => e.Diagnostico)
                    .HasMaxLength(2000)
                    .HasColumnName("DIAGNOSTICO");

                entity.Property(e => e.EspecialistaId).HasColumnName("ESPECIALISTA_ID");

                entity.Property(e => e.ExaminacionInspeccion)
                    .HasMaxLength(2000)
                    .HasColumnName("EXAMINACION_INSPECCION");

                entity.Property(e => e.ExaminacionObservacion)
                    .HasMaxLength(2000)
                    .HasColumnName("EXAMINACION_OBSERVACION");

                entity.Property(e => e.HistoriaId).HasColumnName("HISTORIA_ID");

                entity.HasOne(d => d.Especialista)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.EspecialistaId)
                    .HasConstraintName("FK_RELATIONSHIP_2");

                entity.HasOne(d => d.Historia)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.HistoriaId)
                    .HasConstraintName("FK_RELATIONSHIP_1");
            });

            modelBuilder.Entity<Ejercicio>(entity =>
            {
                entity.ToTable("ejercicio");

                entity.Property(e => e.EjercicioId).HasColumnName("EJERCICIO_ID");

                entity.Property(e => e.EjercicioDescripcion)
                    .HasMaxLength(400)
                    .HasColumnName("EJERCICIO_DESCRIPCION");

                entity.Property(e => e.EjercicioEstado).HasColumnName("EJERCICIO_ESTADO");

                entity.Property(e => e.EjercicioGrafico)
                    .HasMaxLength(3000)
                    .HasColumnName("EJERCICIO_GRAFICO");

                entity.Property(e => e.EjercicioNombre)
                    .HasMaxLength(100)
                    .HasColumnName("EJERCICIO_NOMBRE");
            });

            modelBuilder.Entity<EjercicioTratamiento>(entity =>
            {
                entity.ToTable("ejercicio_tratamiento");

                entity.HasIndex(e => e.TratamientoDiaId, "FK_RELATIONSHIP_10");

                entity.HasIndex(e => e.EjercicioId, "FK_RELATIONSHIP_11");

                entity.Property(e => e.EjercicioTratamientoId).HasColumnName("EJERCICIO_TRATAMIENTO_ID");

                entity.Property(e => e.EjercicioDescanso)
                    .HasMaxLength(100)
                    .HasColumnName("EJERCICIO_DESCANSO");

                entity.Property(e => e.EjercicioEstado).HasColumnName("EJERCICIO_ESTADO");

                entity.Property(e => e.EjercicioId).HasColumnName("EJERCICIO_ID");

                entity.Property(e => e.EjercicioObservacion)
                    .HasMaxLength(2000)
                    .HasColumnName("EJERCICIO_OBSERVACION");

                entity.Property(e => e.EjercicioTratamientoRepeticiones).HasColumnName("EJERCICIO_TRATAMIENTO_REPETICIONES");

                entity.Property(e => e.EjercicioTratamientoSerie).HasColumnName("EJERCICIO_TRATAMIENTO_SERIE");

                entity.Property(e => e.TratamientoDiaId).HasColumnName("TRATAMIENTO_DIA_ID");

                entity.HasOne(d => d.Ejercicio)
                    .WithMany(p => p.EjercicioTratamientos)
                    .HasForeignKey(d => d.EjercicioId)
                    .HasConstraintName("FK_RELATIONSHIP_11");

                entity.HasOne(d => d.TratamientoDia)
                    .WithMany(p => p.EjercicioTratamientos)
                    .HasForeignKey(d => d.TratamientoDiaId)
                    .HasConstraintName("FK_RELATIONSHIP_10");
            });

            modelBuilder.Entity<Evolucion>(entity =>
            {
                entity.ToTable("evolucion");

                entity.HasIndex(e => e.ConsultaId, "FK_RELATIONSHIP_8");

                entity.Property(e => e.EvolucionId).HasColumnName("EVOLUCION_ID");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.EvolucionDescripcion)
                    .HasMaxLength(500)
                    .HasColumnName("EVOLUCION_DESCRIPCION");

                entity.Property(e => e.EvolucionFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("EVOLUCION_FECHA");

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Evolucions)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK_RELATIONSHIP_8");
            });

            modelBuilder.Entity<Foro>(entity =>
            {
                entity.ToTable("foro");

                entity.HasIndex(e => e.ConsultaId, "FK_FORO_CONSULTA");

                entity.Property(e => e.ForoId).HasColumnName("FORO_ID");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Foros)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK_FORO_CONSULTA");
            });

            modelBuilder.Entity<FotosEvolucion>(entity =>
            {
                entity.HasKey(e => e.FotoEvolucionId)
                    .HasName("PRIMARY");

                entity.ToTable("fotos_evolucion");

                entity.HasIndex(e => e.EvolucionId, "FK_RELATIONSHIP_9");

                entity.Property(e => e.FotoEvolucionId).HasColumnName("FOTO_EVOLUCION_ID");

                entity.Property(e => e.EvolucionId).HasColumnName("EVOLUCION_ID");

                entity.Property(e => e.FotoEvolucionDescripcion)
                    .HasMaxLength(2000)
                    .HasColumnName("FOTO_EVOLUCION_DESCRIPCION");

                entity.Property(e => e.FotoEvolucionImagen).HasColumnName("FOTO_EVOLUCION_IMAGEN");

                entity.HasOne(d => d.Evolucion)
                    .WithMany(p => p.FotosEvolucions)
                    .HasForeignKey(d => d.EvolucionId)
                    .HasConstraintName("FK_RELATIONSHIP_9");
            });

            modelBuilder.Entity<FotosExaminacion>(entity =>
            {
                entity.HasKey(e => e.FotoExaminacionId)
                    .HasName("PRIMARY");

                entity.ToTable("fotos_examinacion");

                entity.HasIndex(e => e.ConsultaId, "FK_RELATIONSHIP_5");

                entity.Property(e => e.FotoExaminacionId).HasColumnName("FOTO_EXAMINACION_ID");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.FotoExaminacionDescripcion)
                    .HasMaxLength(2000)
                    .HasColumnName("FOTO_EXAMINACION_DESCRIPCION");

                entity.Property(e => e.FotoExaminacionImagen).HasColumnName("FOTO_EXAMINACION_IMAGEN");

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.FotosExaminacions)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK_RELATIONSHIP_5");
            });

            modelBuilder.Entity<HistoriaClinica>(entity =>
            {
                entity.HasKey(e => e.HistoriaId)
                    .HasName("PRIMARY");

                entity.ToTable("historia_clinica");

                entity.HasIndex(e => e.PacienteId, "FK_RELATIONSHIP_100");

                entity.HasIndex(e => e.EspecialistaId, "FK_RELATIONSHIP_101");

                entity.Property(e => e.HistoriaId).HasColumnName("HISTORIA_ID");

                entity.Property(e => e.EspecialistaId).HasColumnName("ESPECIALISTA_ID");

                entity.Property(e => e.HistoriaActFisica)
                    .HasMaxLength(2000)
                    .HasColumnName("HISTORIA_ACT_FISICA");

                entity.Property(e => e.HistoriaAlergias)
                    .HasMaxLength(2000)
                    .HasColumnName("HISTORIA_ALERGIAS");

                entity.Property(e => e.HistoriaAntecedentes)
                    .HasMaxLength(2000)
                    .HasColumnName("HISTORIA_ANTECEDENTES");

                entity.Property(e => e.HistoriaFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("HISTORIA_FECHA");

                entity.Property(e => e.HistoriaFuente)
                    .HasMaxLength(200)
                    .HasColumnName("HISTORIA_FUENTE");

                entity.Property(e => e.HistoriaHabitos)
                    .HasMaxLength(2000)
                    .HasColumnName("HISTORIA_HABITOS");

                entity.Property(e => e.HistoriaPatologicos)
                    .HasMaxLength(2000)
                    .HasColumnName("HISTORIA_PATOLOGICOS");

                entity.Property(e => e.HistoriaVivienda)
                    .HasMaxLength(2000)
                    .HasColumnName("HISTORIA_VIVIENDA");

                entity.Property(e => e.PacienteId).HasColumnName("PACIENTE_ID");

                entity.HasOne(d => d.Especialista)
                    .WithMany(p => p.HistoriaClinicaEspecialista)
                    .HasForeignKey(d => d.EspecialistaId)
                    .HasConstraintName("FK_RELATIONSHIP_101");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.HistoriaClinicaPacientes)
                    .HasForeignKey(d => d.PacienteId)
                    .HasConstraintName("FK_RELATIONSHIP_100");
            });

            modelBuilder.Entity<HistorialCitum>(entity =>
            {
                entity.HasKey(e => e.HistorialCitaId)
                    .HasName("PRIMARY");

                entity.ToTable("historial_cita");

                entity.HasIndex(e => e.UsuarioId, "FK_RELATIONSHIP_15");

                entity.Property(e => e.HistorialCitaId).HasColumnName("HISTORIAL_CITA_ID");

                entity.Property(e => e.CitaFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("CITA_FECHA");

                entity.Property(e => e.CitaHora)
                    .HasMaxLength(10)
                    .HasColumnName("CITA_HORA");

                entity.Property(e => e.HistorialCitaObservacion)
                    .HasMaxLength(500)
                    .HasColumnName("HISTORIAL_CITA_OBSERVACION");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.HistorialCita)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_RELATIONSHIP_15");
            });

            modelBuilder.Entity<HorarioDia>(entity =>
            {
                entity.HasKey(e => e.HorarioId)
                    .HasName("PRIMARY");

                entity.ToTable("horario_dias");

                entity.Property(e => e.HorarioId).HasColumnName("HORARIO_ID");

                entity.Property(e => e.HorarioNombre)
                    .HasMaxLength(50)
                    .HasColumnName("HORARIO_NOMBRE");
            });

            modelBuilder.Entity<HorarioEspecialistum>(entity =>
            {
                entity.HasKey(e => e.HorarioEspecialistaId)
                    .HasName("PRIMARY");

                entity.ToTable("horario_especialista");

                entity.HasIndex(e => e.EspecialistaId, "FK_RELATIONSHIP_16");

                entity.HasIndex(e => e.HorarioId, "FK_RELATIONSHIP_18");

                entity.HasIndex(e => e.HorarioTrabajoId, "FK_RELATIONSHIP_HORARIOTRABAJO");

                entity.Property(e => e.HorarioEspecialistaId).HasColumnName("HORARIO_ESPECIALISTA_ID");

                entity.Property(e => e.EspecialistaId).HasColumnName("ESPECIALISTA_ID");

                entity.Property(e => e.HorarioEspecialistaEstado).HasColumnName("HORARIO_ESPECIALISTA_ESTADO");

                entity.Property(e => e.HorarioId).HasColumnName("HORARIO_ID");

                entity.Property(e => e.HorarioTrabajoId).HasColumnName("HORARIO_TRABAJO_ID");

                entity.HasOne(d => d.Especialista)
                    .WithMany(p => p.HorarioEspecialista)
                    .HasForeignKey(d => d.EspecialistaId)
                    .HasConstraintName("FK_RELATIONSHIP_16");

                entity.HasOne(d => d.Horario)
                    .WithMany(p => p.HorarioEspecialista)
                    .HasForeignKey(d => d.HorarioId)
                    .HasConstraintName("FK_RELATIONSHIP_18");

                entity.HasOne(d => d.HorarioTrabajo)
                    .WithMany(p => p.HorarioEspecialista)
                    .HasForeignKey(d => d.HorarioTrabajoId)
                    .HasConstraintName("FK_RELATIONSHIP_HORARIOTRABAJO");
            });

            modelBuilder.Entity<HorarioTrabajo>(entity =>
            {
                entity.ToTable("horario_trabajo");

                entity.Property(e => e.HorarioTrabajoId).HasColumnName("HORARIO_TRABAJO_ID");

                entity.Property(e => e.HorarioTrabajoDesde)
                    .HasMaxLength(10)
                    .HasColumnName("HORARIO_TRABAJO_DESDE");

                entity.Property(e => e.HorarioTrabajoHasta)
                    .HasMaxLength(10)
                    .HasColumnName("HORARIO_TRABAJO_HASTA");
            });

            modelBuilder.Entity<Lateralidad>(entity =>
            {
                entity.ToTable("lateralidad");

                entity.Property(e => e.LateralidadId).HasColumnName("LATERALIDAD_ID");

                entity.Property(e => e.LateralidadNombre)
                    .HasMaxLength(200)
                    .HasColumnName("LATERALIDAD_NOMBRE");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("permiso");

                entity.Property(e => e.PermisoId).HasColumnName("PERMISO_ID");

                entity.Property(e => e.PermisoIcon)
                    .HasMaxLength(100)
                    .HasColumnName("PERMISO_ICON");

                entity.Property(e => e.PermisoNombre)
                    .HasMaxLength(200)
                    .HasColumnName("PERMISO_NOMBRE");

                entity.Property(e => e.PermisoPadre).HasColumnName("PERMISO_PADRE");

                entity.Property(e => e.PermisoRuta)
                    .HasMaxLength(100)
                    .HasColumnName("PERMISO_RUTA");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.Property(e => e.RolNombre)
                    .HasMaxLength(200)
                    .HasColumnName("ROL_NOMBRE");
            });

            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.ToTable("rol_permiso");

                entity.HasIndex(e => e.RolId, "FK_ROL_PERMISO");

                entity.HasIndex(e => e.PermisoId, "FK_ROL_PERMISO2");

                entity.Property(e => e.RolPermisoId).HasColumnName("ROL_PERMISO_ID");

                entity.Property(e => e.PermisoId).HasColumnName("PERMISO_ID");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.HasOne(d => d.Permiso)
                    .WithMany(p => p.RolPermisos)
                    .HasForeignKey(d => d.PermisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROL_PERMISO2");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolPermisos)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROL_PERMISO");
            });

            modelBuilder.Entity<Sede>(entity =>
            {
                entity.ToTable("sede");

                entity.Property(e => e.SedeId).HasColumnName("SEDE_ID");

                entity.Property(e => e.SedeDireccion)
                    .HasMaxLength(200)
                    .HasColumnName("SEDE_DIRECCION");

                entity.Property(e => e.SedeEstado).HasColumnName("SEDE_ESTADO");

                entity.Property(e => e.SedeHoraDesde)
                    .HasMaxLength(10)
                    .HasColumnName("SEDE_HORA_DESDE");

                entity.Property(e => e.SedeHoraHasta)
                    .HasMaxLength(10)
                    .HasColumnName("SEDE_HORA_HASTA");

                entity.Property(e => e.SedeNombre)
                    .HasMaxLength(200)
                    .HasColumnName("SEDE_NOMBRE");

                entity.Property(e => e.SedeTelefono)
                    .HasMaxLength(20)
                    .HasColumnName("SEDE_TELEFONO");
            });

            modelBuilder.Entity<Tratamiento>(entity =>
            {
                entity.ToTable("tratamiento");

                entity.HasIndex(e => e.ConsultaId, "FK_RELATIONSHIP_7");

                entity.Property(e => e.TratamientoId).HasColumnName("TRATAMIENTO_ID");

                entity.Property(e => e.ConsultaId).HasColumnName("CONSULTA_ID");

                entity.Property(e => e.TratamientoCompleto).HasColumnName("TRATAMIENTO_COMPLETO");

                entity.Property(e => e.TratamientoDescripcion)
                    .HasMaxLength(2000)
                    .HasColumnName("TRATAMIENTO_DESCRIPCION");

                entity.Property(e => e.TratamientoDias).HasColumnName("TRATAMIENTO_DIAS");

                entity.Property(e => e.TratamientoFase)
                    .HasMaxLength(100)
                    .HasColumnName("TRATAMIENTO_FASE");

                entity.Property(e => e.TratamientoFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("TRATAMIENTO_FECHA_CREACION");

                entity.Property(e => e.TratamientoFechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("TRATAMIENTO_FECHA_INICIO");

                entity.Property(e => e.TratamientoObservacion)
                    .HasMaxLength(3000)
                    .HasColumnName("TRATAMIENTO_OBSERVACION");

                entity.Property(e => e.TratamientoRecomendacion)
                    .HasMaxLength(3000)
                    .HasColumnName("TRATAMIENTO_RECOMENDACION");

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Tratamientos)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK_RELATIONSHIP_7");
            });

            modelBuilder.Entity<TratamientoDium>(entity =>
            {
                entity.HasKey(e => e.TratamientoDiaId)
                    .HasName("PRIMARY");

                entity.ToTable("tratamiento_dia");

                entity.HasIndex(e => e.TratamientoId, "FK_RELATIONSHIP_6");

                entity.Property(e => e.TratamientoDiaId).HasColumnName("TRATAMIENTO_DIA_ID");

                entity.Property(e => e.TratamientoDiaFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("TRATAMIENTO_DIA_FECHA");

                entity.Property(e => e.TratamientoId).HasColumnName("TRATAMIENTO_ID");

                entity.HasOne(d => d.Tratamiento)
                    .WithMany(p => p.TratamientoDia)
                    .HasForeignKey(d => d.TratamientoId)
                    .HasConstraintName("FK_RELATIONSHIP_6");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasIndex(e => e.RolId, "FK_RELATIONSHIP_12");

                entity.HasIndex(e => e.SedeId, "FK_RELATIONSHIP_3");

                entity.HasIndex(e => e.LateralidadId, "FK_RELATIONSHIP_4");

                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");

                entity.Property(e => e.LateralidadId).HasColumnName("LATERALIDAD_ID");

                entity.Property(e => e.RolId).HasColumnName("ROL_ID");

                entity.Property(e => e.SedeId).HasColumnName("SEDE_ID");

                entity.Property(e => e.UsuarioCorreo)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_CORREO");

                entity.Property(e => e.UsuarioDireccion)
                    .HasMaxLength(200)
                    .HasColumnName("USUARIO_DIRECCION");

                entity.Property(e => e.UsuarioEstado).HasColumnName("USUARIO_ESTADO");

                entity.Property(e => e.UsuarioFechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("USUARIO_FECHA_NACIMIENTO");

                entity.Property(e => e.UsuarioIdentificacion)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_IDENTIFICACION");

                entity.Property(e => e.UsuarioNombre)
                    .HasMaxLength(200)
                    .HasColumnName("USUARIO_NOMBRE");

                entity.Property(e => e.UsuarioOcupacion)
                    .HasMaxLength(200)
                    .HasColumnName("USUARIO_OCUPACION");

                entity.Property(e => e.UsuarioPassword)
                    .HasMaxLength(200)
                    .HasColumnName("USUARIO_PASSWORD");

                entity.Property(e => e.UsuarioProfesion)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_PROFESION");

                entity.Property(e => e.UsuarioTelefono)
                    .HasMaxLength(100)
                    .HasColumnName("USUARIO_TELEFONO");

                entity.HasOne(d => d.Lateralidad)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.LateralidadId)
                    .HasConstraintName("FK_RELATIONSHIP_4");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_RELATIONSHIP_12");

                entity.HasOne(d => d.Sede)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.SedeId)
                    .HasConstraintName("FK_RELATIONSHIP_3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
