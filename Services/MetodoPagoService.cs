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

        public async Task<List<MetodoPago>> ObtenerMetodosPago()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MetodoPago
                .Include(m => m.Reserva)
                .AsNoTracking() 
                .ToListAsync();
        }

        public async Task<MetodoPago?> ObtenerMetodoPagoPorId(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.MetodoPago
                .Include(m => m.Reserva)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
        }

        public async Task<bool> CrearMetodoPago(MetodoPago metodoPago)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MetodoPago.Add(metodoPago);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarMetodoPago(MetodoPago metodoPago)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.MetodoPago.Update(metodoPago);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarMetodoPago(int id)
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
