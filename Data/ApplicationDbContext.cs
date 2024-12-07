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

            modelBuilder.Entity<Vehiculo>()
                .HasIndex(v => v.Marca)  // Índice para mejorar búsqueda por marca
                .HasDatabaseName("IX_Vehiculo_Marca");

            modelBuilder.Entity<Vehiculo>()
                .Property(v => v.Imagen)
                .HasMaxLength(500);  // Limitar longitud de la URL de la imagen

            // Configuración para MantenimientoVehiculo
            modelBuilder.Entity<MantenimientoVehiculo>()
                .Property(m => m.Costo)
                .HasColumnType("decimal(18,2)");

            // Configuración para Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany()  // Asumiendo que un cliente puede tener muchas reservas
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);  // Si se elimina un cliente, se eliminan las reservas asociadas

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Vehiculo)
                .WithMany()  // Un vehículo puede estar en muchas reservas
                .HasForeignKey(r => r.VehiculoId)
                .OnDelete(DeleteBehavior.Restrict);  // No permitir la eliminación de un vehículo si está en uso

            modelBuilder.Entity<Reserva>()
            .Property(r => r.TotalPrecio)
            .HasColumnType("decimal(18,2)");

            // Configuración para MetodoPago
            modelBuilder.Entity<MetodoPago>()
                .Property(m => m.Monto)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MetodoPago>()
                .HasOne(mp => mp.Reserva)
                .WithMany()  // Una reserva puede tener muchos pagos
                .HasForeignKey(mp => mp.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);  // Si se elimina una reserva, también se eliminan los pagos asociados

            // Configuración para Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nombres)
                .HasMaxLength(100);  // Limitar la longitud del nombre del cliente

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Email)
                .HasMaxLength(100);  // Limitar la longitud del correo electrónico

            // Configuración para el tipo de pago
            modelBuilder.Entity<MetodoPago>()
                .Property(mp => mp.Tipo)
                .HasConversion(
                  v => v,  // Simplemente guarda el valor como cadena
                  v => v   // Simplemente lee el valor como cadena
                );


            modelBuilder.Entity<MetodoPago>()
            .Property(mp => mp.EstadoTransaccion)
            .HasConversion(
             v => v.ToUpper(),  // Convierte el valor a mayúsculas antes de guardarlo
             v => v.ToLower()   // Convierte el valor a minúsculas cuando se lee
            );   
        }
    }
}
