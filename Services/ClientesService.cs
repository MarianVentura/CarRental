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

        public async Task<List<Clientes>> ObtenerClientes()
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Cliente   
                .AsNoTracking() // Mejor rendimiento sin seguimiento de cambios
                .ToListAsync();
        }

        public async Task<Clientes?> ObtenerClientePorId(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            return await contexto.Cliente
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<bool> CrearCliente(Clientes cliente)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Cliente.Add(cliente);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarCliente(Clientes cliente)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            contexto.Cliente.Update(cliente);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            await using var contexto = await _dbContextFactory.CreateDbContextAsync();
            var cliente = await contexto.Cliente.FindAsync(id);
            if (cliente != null)
            {
                contexto.Cliente.Remove(cliente);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
