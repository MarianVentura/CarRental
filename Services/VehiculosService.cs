using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using BlazorBootstrap;
using CarRental.Extensors;
using CarRental.Models;


namespace CarRental.Services;

public class VehiculosService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ToastService _toastService; 

    public VehiculosService(IDbContextFactory<Contexto> dbFactory, ToastService toastService)
    {
        _dbFactory = dbFactory;
        _toastService = toastService;
    }

    // Obtener todos los vehículos
    public async Task<List<Vehiculo>> ListaVehiculos()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .AsNoTracking()
            .ToListAsync();
    }

    // Obtener un vehículo por su ID
    public async Task<Vehiculo?> ObtenerVehiculoPorId(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VehiculoId == id);

        if (vehiculo == null)
        {
            _toastService.ShowError($"El vehículo con ID {id} no fue encontrado.");
            return null;
        }

        return vehiculo;
    }

    // Agregar un nuevo vehículo
    public async Task<bool> AgregarVehiculo(Vehiculo vehiculo)
    {
        if (vehiculo == null)
        {
            _toastService.ShowError("El vehículo no puede ser nulo.");
            return false;
        }

        if (vehiculo.PrecioPorDia <= 0)
        {
            _toastService.ShowError("El precio por día debe ser mayor a cero.");
            return false;
        }

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.Vehiculos.Add(vehiculo);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Vehículo agregado exitosamente.");
        return true;
    }

    // Actualizar un vehículo existente
    public async Task<bool> ActualizarVehiculo(Vehiculo vehiculo)
    {
        if (vehiculo == null)
        {
            _toastService.ShowError("El vehículo no puede ser nulo.");
            return false;
        }

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var vehiculoExistente = await contexto.Vehiculos.FindAsync(vehiculo.VehiculoId);

        if (vehiculoExistente == null)
        {
            _toastService.ShowError($"El vehículo con ID {vehiculo.VehiculoId} no fue encontrado.");
            return false;
        }

        vehiculoExistente.Marca = vehiculo.Marca;
        vehiculoExistente.Modelo = vehiculo.Modelo;
        vehiculoExistente.Año = vehiculo.Año;
        vehiculoExistente.PrecioPorDia = vehiculo.PrecioPorDia;
        vehiculoExistente.Disponible = vehiculo.Disponible;
        vehiculoExistente.Imagen = vehiculo.Imagen;
        vehiculoExistente.Combustible = vehiculo.Combustible;
        vehiculoExistente.Transmision = vehiculo.Transmision;
        vehiculoExistente.Color = vehiculo.Color;

        contexto.Vehiculos.Update(vehiculoExistente);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Vehículo actualizado exitosamente.");
        return true;
    }

    // Eliminar un vehículo por ID
    public async Task<bool> EliminarVehiculo(int vehiculoId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos.FindAsync(vehiculoId);

        if (vehiculo == null)
        {
            _toastService.ShowError($"El vehículo con ID {vehiculoId} no fue encontrado.");
            return false;
        }

        contexto.Vehiculos.Remove(vehiculo);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Vehículo eliminado exitosamente.");
        return true;
    }

    // Obtener vehículos disponibles
    public async Task<List<Vehiculo>> ObtenerVehiculosDisponibles()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .Where(v => v.Disponible)
            .AsNoTracking()
            .ToListAsync();
    }

    // Buscar vehículos por marca
    public async Task<List<Vehiculo>> BuscarVehiculosPorMarca(string marca)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.Vehiculos
            .Where(v => v.Marca.Contains(marca))
            .AsNoTracking()
            .ToListAsync();
    }

    // Actualizar disponibilidad de un vehículo
    public async Task<bool> ActualizarDisponibilidad(int vehiculoId, bool disponible)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos.FindAsync(vehiculoId);

        if (vehiculo == null)
        {
            _toastService.ShowError($"El vehículo con ID {vehiculoId} no fue encontrado.");
            return false;
        }

        vehiculo.Disponible = disponible;
        contexto.Vehiculos.Update(vehiculo);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Disponibilidad del vehículo actualizada.");
        return true;
    }
}
