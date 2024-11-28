using CarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<MantenimientoVehiculo> MantenimientoVehiculo { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
    }
}
