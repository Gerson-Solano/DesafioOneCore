using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SystemIA.Entity;
namespace SystemIA.DAL.DBContext
{
    public partial class DBIASYSTEMContext : DbContext
    {
        public DBIASYSTEMContext()
        {
        }

        public DBIASYSTEMContext(DbContextOptions<DBIASYSTEMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DocumentInfo> DocumentInfos { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<ProductosFactura> ProductosFacturas { get; set; } = null!;
        public virtual DbSet<Registro> Registros { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentInfo>(entity =>
            {
                entity.HasKey(e => e.IdDocument)
                    .HasName("PK__Document__E3A0F08ECFC138EA");

                entity.ToTable("DocumentInfo");

                entity.Property(e => e.IdDocument).HasColumnName("idDocument");

                entity.Property(e => e.Descripccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripccion");

                entity.Property(e => e.Resumen)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("resumen");

                entity.Property(e => e.Sentimiento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("sentimiento");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.NumFactura)
                    .HasName("PK__Factura__C989668B3703CC56");

                entity.ToTable("Factura");

                entity.Property(e => e.NumFactura)
                    .ValueGeneratedNever()
                    .HasColumnName("numFactura");

                entity.Property(e => e.DireccionCliente)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccionCliente");

                entity.Property(e => e.DireccionProveedor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("direccionProveedor");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreCliente");

                entity.Property(e => e.NombreProveedor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombreProveedor");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ProductosFactura>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__07F4A132E0DD144F");

                entity.ToTable("ProductosFactura");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("idProducto");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumFactura).HasColumnName("numFactura");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("precioUnitario");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total");

                entity.HasOne(d => d.NumFacturaNavigation)
                    .WithMany(p => p.ProductosFacturas)
                    .HasForeignKey(d => d.NumFactura)
                    .HasConstraintName("FK__Productos__numFa__4BAC3F29");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.IdRegistro)
                    .HasName("PK__Registro__62FC8F58E29E912D");

                entity.ToTable("Registro");

                entity.Property(e => e.IdRegistro).HasColumnName("idRegistro");

                entity.Property(e => e.Descripccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripccion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__645723A6171A625D");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreCompleto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
