using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Models;

public partial class CobecaContext : DbContext
{
    public CobecaContext()
    {
    }

    public CobecaContext(DbContextOptions<CobecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Casa> Casas { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Casa>(entity =>
        {
            entity.HasKey(e => e.IdCasa).HasName("PK__casa__C71FDB458D498671");

            entity.ToTable("casa");

            entity.Property(e => e.IdCasa).HasColumnName("id_casa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__registro__48155C1F2FC9FCEA");

            entity.ToTable("registro");

            entity.Property(e => e.IdRegistro).HasColumnName("id_registro");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RefCasa).HasColumnName("ref_casa");

            entity.HasOne(d => d.RefCasaNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.RefCasa)
                .HasConstraintName("FK__registro__ref_ca__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
