using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class MetodoPagoService
{
    private readonly Contexto _context;

    public MetodoPagoService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<MetodoPago>> ObtenerMetodosPagoAsync()
    {
        return await _context.MetodoPago
            .Include(m => m.Reserva)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MetodoPago?> ObtenerMetodoPagoPorIdAsync(int id)
    {
        return await _context.MetodoPago
            .Include(m => m.Reserva)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
    }

    public async Task<bool> CrearMetodoPagoAsync(MetodoPago metodoPago)
    {
        _context.MetodoPago.Add(metodoPago);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarMetodoPagoAsync(MetodoPago metodoPago)
    {
        _context.MetodoPago.Update(metodoPago);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarMetodoPagoAsync(int id)
    {
        var metodoPago = await _context.MetodoPago.FindAsync(id);
        if (metodoPago != null)
        {
            _context.MetodoPago.Remove(metodoPago);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
