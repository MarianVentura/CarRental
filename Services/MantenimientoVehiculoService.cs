using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using BlazorBootstrap;
using CarRental.Extensors;

namespace CarRental.Services;

public class MantenimientoVehiculoService
{
    private readonly IDbContextFactory<Contexto> _dbFactory;
    private readonly ToastService _toastService; 

    public MantenimientoVehiculoService(IDbContextFactory<Contexto> dbFactory, ToastService toastService)
    {
        _dbFactory = dbFactory;
        _toastService = toastService;
    }

    // Obtener todos los mantenimientos de vehículos
    public async Task<List<MantenimientoVehiculo>> ObtenerTodosLosMantenimientos()
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.MantenimientosVehiculo
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    // Obtener un mantenimiento de vehículo por ID
    public async Task<MantenimientoVehiculo?> ObtenerMantenimientoPorId(int id)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var mantenimiento = await contexto.MantenimientosVehiculo
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MantenimientoId == id);

        if (mantenimiento == null)
        {
            _toastService.ShowError($"El mantenimiento con ID {id} no fue encontrado.");
            return null;
        }

        return mantenimiento;
    }

    // Agregar un nuevo mantenimiento de vehículo
    public async Task<bool> AgregarMantenimiento(MantenimientoVehiculo mantenimiento)
    {
        if (mantenimiento == null)
        {
            _toastService.ShowError("El mantenimiento no puede ser nulo.");
            return false;
        }

        if (mantenimiento.Costo <= 0)
        {
            _toastService.ShowError("El costo debe ser un valor mayor a cero.");
            return false;
        }

        if (mantenimiento.FechaMantenimiento == default)
        {
            _toastService.ShowError("La fecha de mantenimiento es obligatoria.");
            return false;
        }

        if (string.IsNullOrEmpty(mantenimiento.Descripcion))
        {
            _toastService.ShowError("La descripción es obligatoria.");
            return false;
        }

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        contexto.MantenimientosVehiculo.Add(mantenimiento);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Mantenimiento de vehículo agregado exitosamente.");
        return true;
    }

    // Actualizar un mantenimiento de vehículo existente
    public async Task<bool> ActualizarMantenimiento(MantenimientoVehiculo mantenimiento)
    {
        if (mantenimiento == null)
        {
            _toastService.ShowError("El mantenimiento no puede ser nulo.");
            return false;
        }

        if (mantenimiento.Costo <= 0)
        {
            _toastService.ShowError("El costo debe ser un valor mayor a cero.");
            return false;
        }

        if (mantenimiento.FechaMantenimiento == default)
        {
            _toastService.ShowError("La fecha de mantenimiento es obligatoria.");
            return false;
        }

        if (string.IsNullOrEmpty(mantenimiento.Descripcion))
        {
            _toastService.ShowError("La descripción es obligatoria.");
            return false;
        }

        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var mantenimientoExistente = await contexto.MantenimientosVehiculo.FindAsync(mantenimiento.MantenimientoId);

        if (mantenimientoExistente == null)
        {
            _toastService.ShowError($"El mantenimiento con ID {mantenimiento.MantenimientoId} no fue encontrado.");
            return false;
        }

        // Actualizar las propiedades del mantenimiento
        mantenimientoExistente.VehiculoId = mantenimiento.VehiculoId;
        mantenimientoExistente.FechaMantenimiento = mantenimiento.FechaMantenimiento;
        mantenimientoExistente.Descripcion = mantenimiento.Descripcion;
        mantenimientoExistente.Costo = mantenimiento.Costo;

        contexto.MantenimientosVehiculo.Update(mantenimientoExistente);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Mantenimiento de vehículo actualizado exitosamente.");
        return true;
    }

    // Eliminar un mantenimiento de vehículo por ID
    public async Task<bool> EliminarMantenimiento(int mantenimientoId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        var mantenimiento = await contexto.MantenimientosVehiculo.FindAsync(mantenimientoId);

        if (mantenimiento == null)
        {
            _toastService.ShowError($"El mantenimiento con ID {mantenimientoId} no fue encontrado.");
            return false;
        }

        contexto.MantenimientosVehiculo.Remove(mantenimiento);
        await contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Mantenimiento de vehículo eliminado exitosamente.");
        return true;
    }

    // Buscar mantenimientos por vehículo
    public async Task<List<MantenimientoVehiculo>> BuscarMantenimientosPorVehiculo(int vehiculoId)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.MantenimientosVehiculo
            .Where(m => m.VehiculoId == vehiculoId)
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    // Obtener los mantenimientos recientes (por ejemplo, los últimos 10)
    public async Task<List<MantenimientoVehiculo>> ObtenerMantenimientosRecientes(int cantidad = 10)
    {
        await using var contexto = await _dbFactory.CreateDbContextAsync();
        return await contexto.MantenimientosVehiculo
            .OrderByDescending(m => m.FechaMantenimiento)
            .Take(cantidad)
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }
}
