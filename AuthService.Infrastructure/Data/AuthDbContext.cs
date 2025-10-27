using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Persona> Personas => Set<Persona>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<PeriodoAcademico> PeriodosAcademicos => Set<PeriodoAcademico>();
        public DbSet<Curso> Cursos => Set<Curso>();
        public DbSet<AsignacionDocente> AsignacionesDocentes => Set<AsignacionDocente>();
        public DbSet<Matricula> Matriculas => Set<Matricula>();
        public DbSet<Nota> Notas => Set<Nota>();
        public DbSet<PadreAlumno> PadresAlumnos => Set<PadreAlumno>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================================================
            // PERSONA ↔ USUARIO (1:1)
            // ============================================================
            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Persona)
                .HasForeignKey<Usuario>(u => u.PersonaID)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices únicos
            modelBuilder.Entity<Persona>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Persona>().HasIndex(u => u.NumeroDocumento).IsUnique();
            modelBuilder.Entity<Persona>().HasIndex(u => u.CodigoPersona).IsUnique();

            // ============================================================
            // ROLES ↔ USUARIOS (1:N)
            // ============================================================
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UsuarioID);

            // ============================================================
            // CURSO ↔ ASIGNACIONES DOCENTES (1:N)
            // ============================================================
            modelBuilder.Entity<AsignacionDocente>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.AsignacionesDocentes)
                .HasForeignKey(a => a.CursoID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // DOCENTE (USUARIO) ↔ ASIGNACIONES DOCENTES (1:N)
            // ============================================================
            modelBuilder.Entity<AsignacionDocente>()
                .HasOne(a => a.Docente)
                .WithMany(u => u.AsignacionesDocentes)
                .HasForeignKey(a => a.DocenteID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // PERIODO ↔ ASIGNACIONES DOCENTES (1:N)
            // ============================================================
            modelBuilder.Entity<AsignacionDocente>()
                .HasOne(a => a.Periodo)
                .WithMany(p => p.AsignacionesDocentes)
                .HasForeignKey(a => a.PeriodoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curso>().HasIndex(c => c.CodigoCurso).IsUnique();

            // ============================================================
            // CURSO ↔ MATRICULAS (1:N)
            // ============================================================
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CursoID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // USUARIO (Alumno) ↔ MATRICULAS (1:N)
            // ============================================================
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Alumno)
                .WithMany(u => u.Matriculas)
                .HasForeignKey(m => m.AlumnoID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // PERIODO ↔ MATRICULAS (1:N)
            // ============================================================
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Periodo)
                .WithMany(p => p.Matriculas)
                .HasForeignKey(m => m.PeriodoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>().HasIndex(m => m.CodigoMatricula).IsUnique();

            // ============================================================
            // MATRICULA ↔ NOTAS (1:N)
            // ============================================================
            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Matricula)
                .WithMany(m => m.Notas)
                .HasForeignKey(n => n.MatriculaID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // PERIODO ↔ NOTAS (1:N)
            // ============================================================
            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Periodo)
                .WithMany(p => p.Notas)
                .HasForeignKey(n => n.PeriodoID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // USUARIO (Docente) ↔ NOTAS (1:N)
            // ============================================================
            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Docente)
                .WithMany(u => u.Notas)
                .HasForeignKey(n => n.DocenteID)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================================================
            // PADRE ↔ ALUMNO (N:N) mediante PADRESALUMNOS
            // ============================================================
            modelBuilder.Entity<PadreAlumno>()
                .HasOne(pa => pa.Padre)
                .WithMany()
                .HasForeignKey(pa => pa.PadreID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PadreAlumno>()
                .HasOne(pa => pa.Alumno)
                .WithMany()
                .HasForeignKey(pa => pa.AlumnoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PeriodoAcademico>().HasKey(p => p.PeriodoID);
            modelBuilder.Entity<AsignacionDocente>().HasKey(p => p.AsignacionID);

            // ============================================================
            // AUDITORÍA Y VALORES POR DEFECTO
            // ============================================================
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.FindProperty("EstadoRegistro") != null)
                    modelBuilder.Entity(entity.ClrType)
                        .Property("EstadoRegistro")
                        .HasDefaultValue(true);

                if (entity.FindProperty("FechaHoraCreacion") != null)
                    modelBuilder.Entity(entity.ClrType)
                        .Property("FechaHoraCreacion")
                        .HasDefaultValueSql("GETDATE()");
            }
        }
    }
}
