using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class VehiculosService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public VehiculosService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Vehiculos>> ObtenerVehiculosAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Vehiculos
                .AsNoTracking() // No se rastrean cambios en este caso
                .ToListAsync();
        }

        public async Task<Vehiculos?> ObtenerVehiculoPorIdAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VehiculosId == id);
        }

        public async Task<bool> CrearVehiculoAsync(Vehiculos vehiculo)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Vehiculos.Add(vehiculo);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarVehiculoAsync(Vehiculos vehiculo)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Vehiculos.Update(vehiculo);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVehiculoAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var vehiculo = await contexto.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                contexto.Vehiculos.Remove(vehiculo);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
