using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NatilleraBE.Models;

namespace NatilleraBE.Data;

public partial class NatilleraDbContext : DbContext
{
    public NatilleraDbContext()
    {
    }

    public NatilleraDbContext(DbContextOptions<NatilleraDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbonosPrestamo> AbonosPrestamos { get; set; }

    public virtual DbSet<Banco> Bancos { get; set; }

    public virtual DbSet<InteresPago> InteresPagos { get; set; }

    public virtual DbSet<InteresPrestamo> InteresPrestamos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Polla> Pollas { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NatilleraDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbonosPrestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Abonos_P__3213E83FF6BECE2E");

            entity.ToTable("Abonos_Prestamo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdPrestamo).HasColumnName("id_prestamo");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");
            entity.Property(e => e.ValorRestante)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_restante");

            entity.HasOne(d => d.IdPrestamoNavigation).WithMany(p => p.AbonosPrestamos)
                .HasForeignKey(d => d.IdPrestamo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Abonos_Pr__id_pr__412EB0B6");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Banco__3213E83FE615938C");

            entity.ToTable("Banco");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ahorro)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ahorro");
            entity.Property(e => e.Cuenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cuenta");
            entity.Property(e => e.Efectivo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("efectivo");
        });

        modelBuilder.Entity<InteresPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Interes___3213E83F85576017");

            entity.ToTable("Interes_Pago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dias).HasColumnName("dias");
            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.ValorTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_total");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.InteresPagos)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Interes_P__id_pa__3B75D760");
        });

        modelBuilder.Entity<InteresPrestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Interes___3213E83FB9984366");

            entity.ToTable("Interes_Prestamo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiasMora).HasColumnName("dias_mora");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdPrestamo).HasColumnName("id_prestamo");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdPrestamoNavigation).WithMany(p => p.InteresPrestamos)
                .HasForeignKey(d => d.IdPrestamo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Interes_P__id_pr__440B1D61");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pago__3213E83FD37F36C5");

            entity.ToTable("Pago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ahorro)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ahorro");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdSocio).HasColumnName("id_socio");
            entity.Property(e => e.Polla)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("polla");
            entity.Property(e => e.Rifa)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("rifa");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pago__id_socio__38996AB5");
        });

        modelBuilder.Entity<Polla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pollas");

            entity.ToTable("Polla");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Mes)
                .HasMaxLength(30)
                .HasColumnName("mes");
            entity.Property(e => e.Numero).HasColumnName("numero");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prestamo__3213E83FED24FE06");

            entity.ToTable("Prestamo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaCorte).HasColumnName("fecha_corte");
            entity.Property(e => e.IdSocio).HasColumnName("id_socio");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prestamo__id_soc__3E52440B");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83F1927BB4F");

            entity.ToTable("Rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Socio__3213E83F1A6A22B8");

            entity.ToTable("Socio");

            entity.HasIndex(e => e.Documento, "UQ__Socio__A25B3E6143BB00FC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .HasColumnName("clave");
            entity.Property(e => e.Documento).HasColumnName("documento");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .HasColumnName("numero");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .HasColumnName("salt");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Socios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Socio__id_rol__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
