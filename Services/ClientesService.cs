using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class ClientesService
{
    private readonly Contexto _context;

    public ClientesService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<Clientes>> ObtenerClientesAsync()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    public async Task<Clientes?> ObtenerClientePorIdAsync(int id)
    {
        return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.ClienteId == id);
    }

    public async Task<bool> CrearClienteAsync(Clientes cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarClienteAsync(Clientes cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarClienteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
