using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaControlEscolar.Models;

public partial class ControlEscolarContext : DbContext
{
    public ControlEscolarContext()
    {
    }

    public ControlEscolarContext(DbContextOptions<ControlEscolarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ControlEscolar;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.ToTable("Alumno");

            entity.Property(e => e.Alumno1)
                .HasMaxLength(100)
                .HasColumnName("Alumno");
            entity.Property(e => e.Correo).HasMaxLength(100);

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Alumno_Profesor");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.Property(e => e.Materia).HasMaxLength(100);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Materia)
                .HasForeignKey(d => d.IdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Materia_Alumno");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.ToTable("Profesor");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Profesor1)
                .HasMaxLength(100)
                .HasColumnName("Profesor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
