using CarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data;

public class Contexto(DbContextOptions<Contexto> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<MetodoPago> MetodoPago { get; set; }
    public DbSet<Vehiculos> Vehiculos { get; set; }
    public DbSet<Categorias> Categorias { get; set; }
    public DbSet<MantenimientoVehiculo> MantenimientoVehiculo { get; set; }
    public DbSet<Reservas> Reservas { get; set; }
    public DbSet<Combustible> Combustible { get; set; }
    public DbSet<Seguros> Seguros { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MantenimientoVehiculo>()
            .Property(m => m.Costo)
            .HasColumnType("decimal(18,2)"); // Define la precisi�n y escala

        modelBuilder.Entity<MetodoPago>()
            .Property(m => m.Monto)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Reservas>()
            .Property(r => r.TotalPrecio)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Vehiculos>()
            .Property(v => v.PrecioPorDia)
            .HasColumnType("decimal(18,2)");
    }


}
