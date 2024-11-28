using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class ReservasService
{
    private readonly Contexto _context;

    public ReservasService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<Reservas>> ObtenerReservasAsync()
    {
        return await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Reservas?> ObtenerReservaPorIdAsync(int id)
    {
        return await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Vehiculo)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReservaId == id);
    }

    public async Task<bool> CrearReservaAsync(Reservas reserva)
    {
        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarReservaAsync(Reservas reserva)
    {
        _context.Reservas.Update(reserva);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarReservaAsync(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva != null)
        {
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
