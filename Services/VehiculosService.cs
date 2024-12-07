using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Data;
using BlazorBootstrap;
using CarRental.Extensors;

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
        try
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Vehiculos.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al obtener vehículos: {ex.Message}");
            return new List<Vehiculo>();
        }
    }

    // Obtener un vehículo por ID
    public async Task<Vehiculo?> ObtenerVehiculoPorId(int id)
    {
        try
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Vehiculos.AsNoTracking().FirstOrDefaultAsync(v => v.VehiculoId == id);
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al obtener el vehículo: {ex.Message}");
            return null;
        }
    }

    // Agregar un nuevo vehículo
    public async Task<bool> AgregarVehiculo(Vehiculo vehiculo)
    {
        if (vehiculo == null)
        {
            _toastService.ShowError("El vehículo no puede ser nulo.");
            return false;
        }

        try
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Vehiculos.Add(vehiculo);
            await contexto.SaveChangesAsync();
            _toastService.ShowSuccess("Vehículo agregado exitosamente.");
            return true;
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al agregar el vehículo: {ex.Message}");
            return false;
        }
    }

    // Actualizar un vehículo existente
    public async Task<bool> ActualizarVehiculo(Vehiculo vehiculo)
    {
        if (vehiculo == null)
        {
            _toastService.ShowError("El vehículo no puede ser nulo.");
            return false;
        }

        try
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var vehiculoExistente = await contexto.Vehiculos.FindAsync(vehiculo.VehiculoId);

            if (vehiculoExistente == null)
            {
                _toastService.ShowError($"El vehículo con ID {vehiculo.VehiculoId} no fue encontrado.");
                return false;
            }

            // Actualizar propiedades
            vehiculoExistente.Marca = vehiculo.Marca;
            vehiculoExistente.Modelo = vehiculo.Modelo;
            vehiculoExistente.Año = vehiculo.Año;
            vehiculoExistente.PrecioPorDia = vehiculo.PrecioPorDia;
            vehiculoExistente.Disponible = vehiculo.Disponible;
            vehiculoExistente.Imagen = vehiculo.Imagen;
            vehiculoExistente.Combustible = vehiculo.Combustible;
            vehiculoExistente.TipoTransmision = vehiculo.TipoTransmision;
            vehiculoExistente.Color = vehiculo.Color;

            contexto.Vehiculos.Update(vehiculoExistente);
            await contexto.SaveChangesAsync();
            _toastService.ShowSuccess("Vehículo actualizado exitosamente.");
            return true;
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al actualizar el vehículo: {ex.Message}");
            return false;
        }
    }

    // Eliminar un vehículo por ID
    public async Task<bool> EliminarVehiculo(int vehiculoId)
    {
        try
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
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al eliminar el vehículo: {ex.Message}");
            return false;
        }
    }

    // Obtener vehículos disponibles
    public async Task<List<Vehiculo>> ObtenerVehiculosDisponibles()
    {
        try
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Vehiculos
                .Where(v => v.Disponible)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al obtener vehículos disponibles: {ex.Message}");
            return new List<Vehiculo>();
        }
    }

    // Buscar vehículos por marca
    public async Task<List<Vehiculo>> BuscarVehiculosPorMarca(string marca)
    {
        if (string.IsNullOrWhiteSpace(marca))
        {
            _toastService.ShowWarning("La marca no puede estar vacía.");
            return new List<Vehiculo>();
        }

        try
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Vehiculos
                .Where(v => v.Marca.Contains(marca))
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al buscar vehículos: {ex.Message}");
            return new List<Vehiculo>();
        }
    }

    // Actualizar disponibilidad de un vehículo
    public async Task<bool> ActualizarDisponibilidad(int vehiculoId, bool disponible)
    {
        try
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
            _toastService.ShowSuccess("Disponibilidad actualizada exitosamente.");
            return true;
        }
        catch (Exception ex)
        {
            _toastService.ShowError($"Error al actualizar disponibilidad: {ex.Message}");
            return false;
        }
    }
}
