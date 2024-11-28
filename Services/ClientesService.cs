using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;

namespace CarRental.Services
{
    public class ClientesService
    {
        private readonly IDbContextFactory<Contexto> _dbContextFactory;

        public ClientesService(IDbContextFactory<Contexto> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Clientes>> ObtenerClientesAsync()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking() // Mejor rendimiento sin seguimiento de cambios
                .ToListAsync();
        }

        public async Task<Clientes?> ObtenerClientePorIdAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<bool> CrearClienteAsync(Clientes cliente)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Clientes.Add(cliente);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarClienteAsync(Clientes cliente)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Clientes.Update(cliente);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente != null)
            {
                contexto.Clientes.Remove(cliente);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
