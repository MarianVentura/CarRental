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
        public DbSet<EstadoReservas> EstadoReservas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Vehiculo)
                .WithMany()
                .HasForeignKey(r => r.VehiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MetodoPago>()
                .HasOne(mp => mp.Reserva)
                .WithMany()
                .HasForeignKey(mp => mp.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MantenimientoVehiculo>()
                .HasOne(mv => mv.Vehiculo)
                .WithMany()
                .HasForeignKey(mv => mv.VehiculoId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<EstadoReservas>().HasData(
               new EstadoReservas { EstadoId = 1, EstadoName = "Pendiente" },
               new EstadoReservas { EstadoId = 2, EstadoName = "Confirmada" },
               new EstadoReservas { EstadoId = 3, EstadoName = "Cancelada" }
           );
        }
    }
}
