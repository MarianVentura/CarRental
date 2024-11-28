using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class MantenimientoVehiculoService
{
    private readonly Contexto _context;

    public MantenimientoVehiculoService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<MantenimientoVehiculo>> ObtenerMantenimientosAsync()
    {
        return await _context.MantenimientoVehiculo
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MantenimientoVehiculo?> ObtenerMantenimientoPorIdAsync(int id)
    {
        return await _context.MantenimientoVehiculo
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MantenimientoId == id);
    }

    public async Task<bool> CrearMantenimientoAsync(MantenimientoVehiculo mantenimiento)
    {
        _context.MantenimientoVehiculo.Add(mantenimiento);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarMantenimientoAsync(MantenimientoVehiculo mantenimiento)
    {
        _context.MantenimientoVehiculo.Update(mantenimiento);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarMantenimientoAsync(int id)
    {
        var mantenimiento = await _context.MantenimientoVehiculo.FindAsync(id);
        if (mantenimiento != null)
        {
            _context.MantenimientoVehiculo.Remove(mantenimiento);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
