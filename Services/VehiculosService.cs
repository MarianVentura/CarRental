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

        public async Task<List<Vehiculos>> ObtenerVehiculos()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Vehiculo
                .AsNoTracking() // No se rastrean cambios en este caso
                .ToListAsync();
        }

        public async Task<Vehiculos?> ObtenerVehiculoPorId(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Vehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VehiculoId == id);
        }
        public async Task<List<Vehiculos>> ObtenerVehiculosPorCategoria(string categoria)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Vehiculo
                .Where(v => v.Categoria.Contains(categoria))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CrearVehiculo(Vehiculos vehiculo)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Vehiculo.Add(vehiculo);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarVehiculo(Vehiculos vehiculo)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Vehiculo.Update(vehiculo);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVehiculo(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var vehiculo = await contexto.Vehiculo.FindAsync(id);
            if (vehiculo != null)
            {
                contexto.Vehiculo.Remove(vehiculo);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
