using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class CategoriasService
{
    private readonly Contexto _context;

    public CategoriasService(Contexto context)
    {
        _context = context;
    }

    public async Task<List<Categorias>> ObtenerCategoriasAsync()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<Categorias?> ObtenerCategoriaPorIdAsync(int id)
    {
        return await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
    }

    public async Task<bool> CrearCategoriaAsync(Categorias categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarCategoriaAsync(Categorias categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarCategoriaAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
