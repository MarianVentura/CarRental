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

        public async Task<List<MantenimientoVehiculo>> ObtenerMantenimientos()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MantenimientoVehiculo
                .Include(m => m.Vehiculo)
                .AsNoTracking() 
                .ToListAsync();
        }

        public async Task<MantenimientoVehiculo?> ObtenerMantenimientoPorId(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MantenimientoVehiculo
                .Include(m => m.Vehiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MantenimientoId == id);
        }

        public async Task<bool> CrearMantenimiento(MantenimientoVehiculo mantenimiento)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MantenimientoVehiculo.Add(mantenimiento);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarMantenimiento(MantenimientoVehiculo mantenimiento)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MantenimientoVehiculo.Update(mantenimiento);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarMantenimiento(int id)
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
