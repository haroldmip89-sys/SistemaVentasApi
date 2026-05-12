using Microsoft.EntityFrameworkCore;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Infraestructura.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    #region DbSets

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<ProductoCategoria> ProductoCategorias => Set<ProductoCategoria>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<Compra> Compras => Set<Compra>();
    public DbSet<DetalleCompra> DetallesCompra => Set<DetalleCompra>();
    public DbSet<MovimientoStock> MovimientosStock => Set<MovimientoStock>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<Comprobante> Comprobantes => Set<Comprobante>();

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* =========================
           USUARIO
        ==========================*/
        modelBuilder.Entity<Usuario>(builder =>
        {
            //builder.ToTable("USUARIO");
            builder.ToTable("usuario");
            builder.HasKey(x => x.IdUsuario);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Rol)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Estado)
                .HasDefaultValue(true);
        });

        /* =========================
           CATEGORIA
        ==========================*/
        modelBuilder.Entity<Categoria>(builder =>
        {
            //builder.ToTable("CATEGORIA");
            builder.ToTable("categoria");
            builder.HasKey(x => x.IdCategoria);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasMaxLength(255);

            builder.Property(x => x.ColorHex)
                .HasMaxLength(10);

            builder.Property(x => x.Estado)
                .HasDefaultValue(true);
        });

        /* =========================
           MARCA
        ==========================*/
        modelBuilder.Entity<Marca>(builder =>
        {
            //builder.ToTable("MARCA");
            builder.ToTable("marca");
            builder.HasKey(x => x.IdMarca);

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasMaxLength(255);

            builder.Property(x => x.ColorHex)
                .HasMaxLength(10);

            builder.Property(x => x.Estado)
                .HasDefaultValue(true);
        });

        /* =========================
           PRODUCTO
        ==========================*/
        modelBuilder.Entity<Producto>(builder =>
        {
            //builder.ToTable("PRODUCTO");
            builder.ToTable("producto");
            builder.HasKey(x => x.IdProducto);

            builder.Property(x => x.Nombre)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasMaxLength(255);

            builder.Property(x => x.Imagen)
                .HasMaxLength(255);

            builder.Property(x => x.PrecioVenta)
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.CostoPromedio)
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.StockActual)
                .HasDefaultValue(0);

            builder.Property(x => x.Estado)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Marca)
                .WithMany(m => m.Productos)
                .HasForeignKey(x => x.IdMarca);
        });

        /* =========================
           PRODUCTO_CATEGORIA
        ==========================*/
        modelBuilder.Entity<ProductoCategoria>(builder =>
        {
            //builder.ToTable("PRODUCTO_CATEGORIA");
            builder.ToTable("producto_categoria");
            builder.HasKey(x => new { x.IdProducto, x.IdCategoria });

            builder.HasOne(x => x.Producto)
                .WithMany(p => p.Categorias)
                .HasForeignKey(x => x.IdProducto);

            builder.HasOne(x => x.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(x => x.IdCategoria);
        });

        /* =========================
           PROVEEDOR
        ==========================*/
        modelBuilder.Entity<Proveedor>(builder =>
        {
            //builder.ToTable("PROVEEDOR");
            builder.ToTable("proveedor");
            builder.HasKey(x => x.IdProveedor);

            builder.Property(x => x.RazonSocial)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.RUC)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Telefono)
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasMaxLength(100);

            builder.Property(x => x.Estado)
                .HasDefaultValue(true);
        });

        /* =========================
           COMPRA
        ==========================*/
        modelBuilder.Entity<Compra>(builder =>
        {
            //builder.ToTable("COMPRA");
            builder.ToTable("compra");
            builder.HasKey(x => x.IdCompra);

            builder.Property(x => x.Total)
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.Estado)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.FechaCompra)
                //.HasDefaultValueSql("GETDATE()");
                .HasDefaultValueSql("NOW()");

            builder.HasOne(x => x.Proveedor)
                .WithMany(p => p.Compras)
                .HasForeignKey(x => x.IdProveedor);

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Compras)
                .HasForeignKey(x => x.IdUsuario);
        });

        /* =========================
           DETALLE_COMPRA
        ==========================*/
        modelBuilder.Entity<DetalleCompra>(builder =>
        {
            //builder.ToTable("DETALLE_COMPRA");
            builder.ToTable("detalle_compra");
            builder.HasKey(x => x.IdDetalleCompra);

            builder.Property(x => x.PrecioCompra)
                .HasColumnType("numeric(10,2)");

            //  RELACIÓN CON COMPRA
            builder.HasOne(x => x.Compra)
                .WithMany(c => c.Detalles)
                .HasForeignKey(x => x.IdCompra);

            //  RELACIÓN CON PRODUCTO
            builder.HasOne(x => x.Producto)
                .WithMany(p => p.DetallesCompra)
                .HasForeignKey(x => x.IdProducto);

        });

        /* =========================
           MOVIMIENTO_STOCK
        ==========================*/
        modelBuilder.Entity<MovimientoStock>(builder =>
        {
            //builder.ToTable("MOVIMIENTO_STOCK");
            builder.ToTable("movimiento_stock");
            builder.HasKey(x => x.IdMovimiento);

            builder.Property(x => x.Tipo)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.Origen)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasOne(x => x.Producto)
                .WithMany(p => p.Movimientos)
                .HasForeignKey(x => x.IdProducto);

            builder.Property(x => x.Fecha)
                //.HasDefaultValueSql("GETDATE()");
                .HasDefaultValueSql("NOW()");
        });

        /* =========================
           VENTA
        ==========================*/
        modelBuilder.Entity<Venta>(builder =>
        {
            //builder.ToTable("VENTA");
            builder.ToTable("venta");
            builder.HasKey(x => x.IdVenta);

            builder.Property(x => x.Comprobante)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.TipoDocumento)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(x => x.EstadoVenta)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.Total)
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.FechaVenta)
                //.HasDefaultValueSql("GETDATE()");
                .HasDefaultValueSql("NOW()");

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Ventas)
                .HasForeignKey(x => x.IdUsuario);

            builder.HasOne(v => v.ComprobanteElectronico)
                .WithOne(c => c.Venta)
                .HasForeignKey<Comprobante>(c => c.IdVenta);
        });

        /* =========================
           DETALLE_VENTA
        ==========================*/
        modelBuilder.Entity<DetalleVenta>(builder =>
        {
            //builder.ToTable("DETALLE_VENTA");
            builder.ToTable("detalle_venta");
            builder.HasKey(x => x.IdDetalleVenta);

            builder.Property(x => x.PrecioUnitario)
                .HasColumnType("numeric(10,2)");

            //  RELACIÓN CON VENTA
            builder.HasOne(x => x.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(x => x.IdVenta);

            //  RELACIÓN CON PRODUCTO
            builder.HasOne(x => x.Producto)
                .WithMany(p => p.DetallesVenta)
                .HasForeignKey(x => x.IdProducto);
        });

        /* =========================
           PAGO
        ==========================*/
        modelBuilder.Entity<Pago>(builder =>
        {
            //builder.ToTable("PAGO");
            builder.ToTable("pago");
            builder.HasKey(x => x.IdPago);

            builder.Property(x => x.MetodoPago)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(x => x.EstadoPago)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.FechaPago)
                //.HasDefaultValueSql("GETDATE()");
                .HasDefaultValueSql("NOW()");
            builder.Property(x => x.Monto)
                //.HasPrecision(18, 2); //  CLAVE
                .HasColumnType("numeric(10,2)");

            builder.HasOne(p => p.Venta)
                .WithMany(v => v.Pagos)
                .HasForeignKey(p => p.IdVenta);
        });

        /* =========================
           COMPROBANTE
        ==========================*/
        modelBuilder.Entity<Comprobante>(builder =>
        {
            //builder.ToTable("COMPROBANTE");
            builder.ToTable("comprobante");
            builder.HasKey(x => x.IdComprobante);

            builder.Property(x => x.Tipo)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.Estado)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(x => x.FechaEmision)
                //.HasDefaultValueSql("GETDATE()");
                .HasDefaultValueSql("NOW()");
        });
    }
}
