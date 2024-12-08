using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Data;
using System.Linq.Expressions;

namespace CarRental.Services;

public class VehiculosService
{
    private readonly Contexto _contexto;

    public VehiculosService(Contexto contexto)
    {
        _contexto = contexto;
    }

    // Verificar si existe un vehículo por ID
    public async Task<bool> Existe(int vehiculoId)
    {
        return await _contexto.Vehiculos.AnyAsync(v => v.VehiculoId == vehiculoId);
    }

    // Insertar un nuevo vehículo
    private async Task<bool> Insertar(Vehiculo vehiculo)
    {
        _contexto.Vehiculos.Add(vehiculo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Modificar un vehículo existente
    private async Task<bool> Modificar(Vehiculo vehiculo)
    {
        _contexto.Update(vehiculo);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Guardar un vehículo (insertar o modificar)
    public async Task<bool> Guardar(Vehiculo vehiculo)
    {
        if (!await Existe(vehiculo.VehiculoId))
        {
            return await Insertar(vehiculo);
        }
        else
        {
            return await Modificar(vehiculo);
        }
    }

    // Eliminar un vehículo
    public async Task<bool> EliminarVehiculo(Vehiculo vehiculo)
    {
        return await _contexto.Vehiculos
            .AsNoTracking()
            .Where(v => v.VehiculoId == vehiculo.VehiculoId)
            .ExecuteDeleteAsync() > 0;
    }

    // Buscar un vehículo por ID
    public async Task<Vehiculo?> Buscar(int vehiculoId)
    {
        return await _contexto.Vehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId);
    }

    // Buscar vehículos por marca
    public async Task<List<Vehiculo>> BuscarPorMarca(string marca)
    {
        if (string.IsNullOrWhiteSpace(marca))
        {
            return new List<Vehiculo>();
        }

        return await _contexto.Vehiculos
            .Where(v => v.Marca.Contains(marca))
            .AsNoTracking()
            .ToListAsync();
    }

    // Listar vehículos con un criterio específico
    public async Task<List<Vehiculo>> ListaVehiculos()
    {
        return await _contexto.Vehiculos
            .AsNoTracking()
            .ToListAsync();
    }

    // Obtener todos los vehículos
    public async Task<List<Vehiculo>> ObtenerVehiculos()
    {
        return await _contexto.Vehiculos
            .AsNoTracking()
            .ToListAsync();
    }
}
