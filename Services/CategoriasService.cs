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

        public async Task<List<Categorias>> ObtenerCategorias()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Categoria
                .AsNoTracking() 
                .ToListAsync();
        }

        public async Task<Categorias?> ObtenerCategoriaPorId(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Categoria
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoriaId == id);
        }

        public async Task<bool> CrearCategoria(Categorias categoria)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Categoria.Add(categoria);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarCategoria(Categorias categoria)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Categoria.Update(categoria);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCategoria(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var categoria = await contexto.Categoria.FindAsync(id);
            if (categoria != null)
            {
                contexto.Categoria.Remove(categoria);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
