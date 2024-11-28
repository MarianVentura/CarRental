using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class MetodoPagoService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public MetodoPagoService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<MetodoPago>> ObtenerMetodosPagoAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MetodoPago
                .Include(m => m.Reserva)
                .AsNoTracking() // Mejor rendimiento sin seguimiento de cambios
                .ToListAsync();
        }

        public async Task<MetodoPago?> ObtenerMetodoPagoPorIdAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MetodoPago
                .Include(m => m.Reserva)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
        }

        public async Task<bool> CrearMetodoPagoAsync(MetodoPago metodoPago)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MetodoPago.Add(metodoPago);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarMetodoPagoAsync(MetodoPago metodoPago)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MetodoPago.Update(metodoPago);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarMetodoPagoAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var metodoPago = await contexto.MetodoPago.FindAsync(id);
            if (metodoPago != null)
            {
                contexto.MetodoPago.Remove(metodoPago);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
