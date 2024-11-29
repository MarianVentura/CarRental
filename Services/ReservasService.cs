using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class ReservasService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public ReservasService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Reservas>> ObtenerReservas()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking() // No se rastrean cambios para mejorar rendimiento
                .ToListAsync();
        }

        public async Task<Reservas?> ObtenerReservaPorId(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task<bool> CrearReserva(Reservas reserva)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Reservas.Add(reserva);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarReserva(Reservas reserva)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Reservas.Update(reserva);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarReserva(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var reserva = await contexto.Reservas.FindAsync(id);
            if (reserva != null)
            {
                contexto.Reservas.Remove(reserva);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
