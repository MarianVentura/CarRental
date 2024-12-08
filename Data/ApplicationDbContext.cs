using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;

namespace CarRental.Data
{
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

            // Configuración para Vehículo
            modelBuilder.Entity<Vehiculo>()
                .Property(v => v.PrecioPorDia)
                .HasColumnType("decimal(18,2)");
        
            // Configuración para MantenimientoVehiculo
            modelBuilder.Entity<MantenimientoVehiculo>()
                .Property(m => m.Costo)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Reserva>()
            .Property(r => r.TotalPrecio)
            .HasColumnType("decimal(18,2)");

            // Configuración para MetodoPago
            modelBuilder.Entity<MetodoPago>()
                .Property(m => m.Monto)
                .HasColumnType("decimal(18,2)");
        }
    }
}
