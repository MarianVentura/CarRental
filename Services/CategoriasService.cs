using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class CategoriasService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public CategoriasService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Categorias>> ObtenerCategoriasAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Categorias
                .AsNoTracking() // Mejor rendimiento sin seguimiento de cambios
                .ToListAsync();
        }

        public async Task<Categorias?> ObtenerCategoriaPorIdAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<bool> CrearCategoriaAsync(Categorias categoria)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Categorias.Add(categoria);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarCategoriaAsync(Categorias categoria)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Categorias.Update(categoria);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCategoriaAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var categoria = await contexto.Categorias.FindAsync(id);
            if (categoria != null)
            {
                contexto.Categorias.Remove(categoria);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
