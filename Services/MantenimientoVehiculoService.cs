using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using BlazorBootstrap;
using CarRental.Extensors;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Services;

public class MantenimientoVehiculoService
{
    private readonly Contexto _contexto;
    private readonly ToastService _toastService;

    public MantenimientoVehiculoService(Contexto contexto, ToastService toastService)
    {
        _contexto = contexto;
        _toastService = toastService;
    }

    // Obtener todos los mantenimientos de vehículos
    public async Task<List<MantenimientoVehiculo>> ObtenerTodosLosMantenimientos()
    {
        return await _contexto.MantenimientosVehiculo
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    // Obtener un mantenimiento de vehículo por ID
    public async Task<MantenimientoVehiculo?> ObtenerMantenimientoPorId(int id)
    {
        var mantenimiento = await _contexto.MantenimientosVehiculo
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MantenimientoId == id);

        if (mantenimiento == null)
        {
            _toastService.ShowError($"El mantenimiento con ID {id} no fue encontrado.");
        }

        return mantenimiento;
    }

    // Agregar un nuevo mantenimiento de vehículo
    public async Task<bool> AgregarMantenimiento(MantenimientoVehiculo mantenimiento)
    {
        var errores = ValidarMantenimiento(mantenimiento);
        if (errores.Any())
        {
            MostrarErrores(errores);
            return false;
        }

        _contexto.MantenimientosVehiculo.Add(mantenimiento);
        await _contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Mantenimiento de vehículo agregado exitosamente.");
        return true;
    }

    // Actualizar un mantenimiento de vehículo existente
    public async Task<bool> ActualizarMantenimiento(MantenimientoVehiculo mantenimiento)
    {
        var errores = ValidarMantenimiento(mantenimiento);
        if (errores.Any())
        {
            MostrarErrores(errores);
            return false;
        }

        var mantenimientoExistente = await _contexto.MantenimientosVehiculo.FindAsync(mantenimiento.MantenimientoId);

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

        _contexto.MantenimientosVehiculo.Update(mantenimientoExistente);
        await _contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Mantenimiento de vehículo actualizado exitosamente.");
        return true;
    }

    // Eliminar un mantenimiento de vehículo por ID
    public async Task<bool> EliminarMantenimiento(int mantenimientoId)
    {
        var mantenimiento = await _contexto.MantenimientosVehiculo.FindAsync(mantenimientoId);

        if (mantenimiento == null)
        {
            _toastService.ShowError($"El mantenimiento con ID {mantenimientoId} no fue encontrado.");
            return false;
        }

        _contexto.MantenimientosVehiculo.Remove(mantenimiento);
        await _contexto.SaveChangesAsync();

        _toastService.ShowSuccess("Mantenimiento de vehículo eliminado exitosamente.");
        return true;
    }

    // Buscar mantenimientos por vehículo
    public async Task<List<MantenimientoVehiculo>> BuscarMantenimientosPorVehiculo(int vehiculoId)
    {
        return await _contexto.MantenimientosVehiculo
            .Where(m => m.VehiculoId == vehiculoId)
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    // Obtener los mantenimientos recientes (por ejemplo, los últimos 10)
    public async Task<List<MantenimientoVehiculo>> ObtenerMantenimientosRecientes(int cantidad = 10)
    {
        return await _contexto.MantenimientosVehiculo
            .OrderByDescending(m => m.FechaMantenimiento)
            .Take(cantidad)
            .Include(m => m.Vehiculo)
            .AsNoTracking()
            .ToListAsync();
    }

    // Validar un mantenimiento de vehículo
    private List<string> ValidarMantenimiento(MantenimientoVehiculo mantenimiento)
    {
        var context = new ValidationContext(mantenimiento);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(mantenimiento, context, results, true);

        return results.Select(r => r.ErrorMessage).ToList();
    }

    // Mostrar errores en los mensajes Toast
    private void MostrarErrores(IEnumerable<string> errores)
    {
        foreach (var error in errores)
        {
            _toastService.ShowError(error);
        }
    }
}
