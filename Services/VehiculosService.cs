using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class VehiculosService
{
    private readonly Contexto _context;

    public VehiculosService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<Vehiculos>> ObtenerVehiculosAsync()
    {
        return await _context.Vehiculos.AsNoTracking().ToListAsync();
    }

    public async Task<Vehiculos?> ObtenerVehiculoPorIdAsync(int id)
    {
        return await _context.Vehiculos.AsNoTracking().FirstOrDefaultAsync(v => v.VehiculosId == id);
    }

    public async Task<bool> CrearVehiculoAsync(Vehiculos vehiculo)
    {
        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarVehiculoAsync(Vehiculos vehiculo)
    {
        _context.Vehiculos.Update(vehiculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarVehiculoAsync(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo != null)
        {
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
