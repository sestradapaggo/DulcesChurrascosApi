using Microsoft.EntityFrameworkCore;
using DulcesChurrascosAPI.Models;

namespace DulcesChurrascosAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Churrasco> Churrascos { get; set; }
    public DbSet<Dulce> Dulces { get; set; }
    public DbSet<Combo> Combos { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Producto>()
             .Property(p => p.Tipo)
             .HasConversion<string>();
        model.Entity<Producto>()
             .Property(p => p.Modalidad)
             .HasConversion<string>();

        model.Entity<Churrasco>()
             .Property(c => c.Porciones)
             .HasConversion<int>();

        model.Entity<Churrasco>()
            .Property(c => c.Modalidad)
            .HasConversion<string>();

        base.OnModelCreating(model);
    }
}
