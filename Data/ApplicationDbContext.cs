using CarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data;

public class Contexto(DbContextOptions<Contexto> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<MetodoPago> MetodoPago { get; set; }
    public DbSet<Vehiculo> Vehiculo { get; set; }
    public DbSet<MantenimientoVehiculo> MantenimientoVehiculo { get; set; }
    public DbSet<Reserva> Reservas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MantenimientoVehiculo>()
            .Property(m => m.Costo)
            .HasColumnType("decimal(18,2)"); 

        modelBuilder.Entity<MetodoPago>()
            .Property(m => m.Monto)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Reserva>()
            .Property(r => r.TotalPrecio)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Vehiculo>()
            .Property(v => v.PrecioPorDia)
            .HasColumnType("decimal(18,2)");

        

        

       
    }


}
