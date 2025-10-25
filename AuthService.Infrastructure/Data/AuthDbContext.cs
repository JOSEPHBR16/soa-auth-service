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

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<PadreAlumno> PadresAlumnos { get; set; }
        public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }

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

            

            // ============================================================
            // USUARIO (Docente) ↔ CURSOS (1:N)
            // ============================================================
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Docente)
                .WithMany() // o .WithMany(u => u.CursosDictados)
                .HasForeignKey(c => c.DocenteID)
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
                .WithMany() // o .WithMany(u => u.Matriculas)
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
                .WithMany() // o .WithMany(u => u.NotasRegistradas)
                .HasForeignKey(n => n.UsuarioDocenteID)
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

            // ============================================================
            // CONFIGURACIONES ADICIONALES (Auditoría y defaults)
            // ============================================================
            modelBuilder.Entity<Curso>()
                .Property(c => c.FechaHoraCreacion)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Curso>()
                .Property(c => c.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<Matricula>()
                .Property(m => m.FechaHoraCreacion)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Matricula>()
                .Property(m => m.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<Nota>()
                .Property(n => n.FechaHoraCreacion)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Nota>()
                .Property(n => n.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<PeriodoAcademico>()
                .Property(p => p.FechaHoraCreacion)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<PeriodoAcademico>()
                .Property(p => p.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<PadreAlumno>()
                .Property(p => p.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<Rol>()
                .Property(p => p.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<Usuario>()
                .Property(p => p.EstadoRegistro)
                .HasDefaultValue(true);

            modelBuilder.Entity<Persona>()
                .Property(p => p.EstadoRegistro)
                .HasDefaultValue(true);
        }
    }
}
