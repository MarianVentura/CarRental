using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class CombustibleService
{
    private readonly IDbContextFactory<Contexto> _dbContextFactory;

    public CombustibleService(IDbContextFactory<Contexto> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Combustible>> ObtenerCombustibles()
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.Combustible
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Combustible?> ObtenerCombustiblePorId(int id)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.Combustible
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CombustibleId == id);
    }

    public async Task<bool> CrearCombustible(Combustible combustible)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();

        // Ejemplo: Comprobar si ya existe un combustible con el mismo nombre
        if (await contexto.Combustible.AnyAsync(c => c.Tipo == combustible.Tipo))
        {
            // Aquí puedes devolver un resultado de error o lanzar una excepción
            return false;
        }

        contexto.Combustible.Add(combustible);
        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarCombustible(Combustible combustible)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        contexto.Combustible.Update(combustible);
        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarCombustible(int id)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        var combustible = await contexto.Combustible.FindAsync(id);
        if (combustible != null)
        {
            contexto.Combustible.Remove(combustible);
            await contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
