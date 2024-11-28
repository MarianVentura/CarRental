using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class MantenimientoVehiculoService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public MantenimientoVehiculoService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<MantenimientoVehiculo>> ObtenerMantenimientosAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MantenimientoVehiculo
                .Include(m => m.Vehiculo)
                .AsNoTracking() // Mejor rendimiento sin seguimiento de cambios
                .ToListAsync();
        }

        public async Task<MantenimientoVehiculo?> ObtenerMantenimientoPorIdAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MantenimientoVehiculo
                .Include(m => m.Vehiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MantenimientoId == id);
        }

        public async Task<bool> CrearMantenimientoAsync(MantenimientoVehiculo mantenimiento)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MantenimientoVehiculo.Add(mantenimiento);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarMantenimientoAsync(MantenimientoVehiculo mantenimiento)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MantenimientoVehiculo.Update(mantenimiento);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarMantenimientoAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var mantenimiento = await contexto.MantenimientoVehiculo.FindAsync(id);
            if (mantenimiento != null)
            {
                contexto.MantenimientoVehiculo.Remove(mantenimiento);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
