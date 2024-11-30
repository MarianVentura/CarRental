using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services;

public class SegurosService
{
    private readonly IDbContextFactory<Contexto> _dbContextFactory;

    public SegurosService(IDbContextFactory<Contexto> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Seguros>> ObtenerSeguros()
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.Seguros
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Seguros?> ObtenerSeguroPorId(int id)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        return await contexto.Seguros
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SeguroId == id);
    }

    public async Task<bool> CrearSeguro(Seguros seguro)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        contexto.Seguros.Add(seguro);
        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActualizarSeguro(Seguros seguro)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        contexto.Seguros.Update(seguro);
        await contexto.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarSeguro(int id)
    {
        await using var contexto = await _dbContextFactory.CreateDbContextAsync();
        var seguro = await contexto.Seguros.FindAsync(id);
        if (seguro != null)
        {
            contexto.Seguros.Remove(seguro);
            await contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
