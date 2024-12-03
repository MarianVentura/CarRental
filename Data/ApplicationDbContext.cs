using CarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data;

public class Contexto : IdentityDbContext<ApplicationUser>
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<MetodoPago> MetodosPago { get; set; }
    public DbSet<MantenimientoVehiculo> MantenimientosVehiculo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración para MantenimientoVehiculo
        modelBuilder.Entity<MantenimientoVehiculo>()
            .Property(m => m.Costo)
            .HasColumnType("decimal(18,2)");

        // Configuración para Vehiculo
        modelBuilder.Entity<Vehiculo>()
            .Property(v => v.PrecioPorDia)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Vehiculo>()
            .Property(v => v.Combustible)
            .HasConversion<string>(); // Almacena enum como texto

        modelBuilder.Entity<Vehiculo>()
            .Property(v => v.Transmision)
            .HasConversion<string>(); // Almacena enum como texto

        // Configuración para Reserva
        modelBuilder.Entity<Reserva>()
            .Property(r => r.TotalPrecio)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Reserva>()
            .Property(r => r.Estado)
            .HasConversion<string>(); // Almacena enum como texto

        // Configuración para MetodoPago
        modelBuilder.Entity<MetodoPago>()
            .Property(m => m.Monto)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<MetodoPago>()
            .Property(m => m.Tipo)
            .HasConversion<string>(); // Almacena enum como texto

        modelBuilder.Entity<MetodoPago>()
            .Property(m => m.EstadoTransaccion)
            .HasConversion<string>(); // Almacena enum como texto

        // Configuración para Cliente
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Identificacion)
            .HasConversion<string>(); // Almacena enum como texto
    }
}