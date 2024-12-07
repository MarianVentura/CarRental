using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using BlazorBootstrap;
using System.Linq;
using CarRental.Extensors;

namespace CarRental.Services
{
    public class ReservasService
    {
        private readonly IDbContextFactory<Contexto> _dbFactory;
        private readonly ToastService _toastService;

        public ReservasService(IDbContextFactory<Contexto> dbFactory, ToastService toastService)
        {
            _dbFactory = dbFactory;
            _toastService = toastService;
        }

        // Obtener todas las reservas
        public async Task<List<Reserva>> ObtenerTodasLasReservas()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener una reserva por su ID
        public async Task<Reserva?> ObtenerReservaPorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var reserva = await contexto.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReservaId == id);

            if (reserva == null)
            {
                _toastService.ShowError($"La reserva con ID {id} no fue encontrada.");
                return null;
            }

            return reserva;
        }

        // Agregar una nueva reserva
        public async Task<bool> AgregarReserva(Reserva reserva)
        {
            if (reserva == null)
            {
                _toastService.ShowError("La reserva no puede ser nula.");
                return false;
            }

            // Validaciones de negocio
            if (reserva.FechaRecogida >= reserva.FechaDevolucion)
            {
                _toastService.ShowError("La fecha de recogida debe ser anterior a la fecha de devolución.");
                return false;
            }

            if (reserva.TotalPrecio <= 0)
            {
                _toastService.ShowError("El precio total debe ser mayor a cero.");
                return false;
            }

            await using var contexto = await _dbFactory.CreateDbContextAsync();

            // Validar existencia de Cliente y Vehículo
            var clienteExiste = await contexto.Clientes.AnyAsync(c => c.ClienteId == reserva.ClienteId);
            var vehiculoExiste = await contexto.Vehiculos.AnyAsync(v => v.VehiculoId == reserva.VehiculoId);

            if (!clienteExiste)
            {
                _toastService.ShowError("El cliente seleccionado no existe.");
                return false;
            }

            if (!vehiculoExiste)
            {
                _toastService.ShowError("El vehículo seleccionado no existe.");
                return false;
            }

            contexto.Reservas.Add(reserva);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Reserva agregada exitosamente.");
            return true;
        }

        // Actualizar una reserva existente
        public async Task<bool> ActualizarReserva(Reserva reserva)
        {
            if (reserva == null)
            {
                _toastService.ShowError("La reserva no puede ser nula.");
                return false;
            }

            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var reservaExistente = await contexto.Reservas.FindAsync(reserva.ReservaId);

            if (reservaExistente == null)
            {
                _toastService.ShowError($"La reserva con ID {reserva.ReservaId} no fue encontrada.");
                return false;
            }

            // Validaciones
            if (reserva.FechaRecogida >= reserva.FechaDevolucion)
            {
                _toastService.ShowError("La fecha de recogida debe ser anterior a la fecha de devolución.");
                return false;
            }

            if (reserva.TotalPrecio <= 0)
            {
                _toastService.ShowError("El precio total debe ser mayor a cero.");
                return false;
            }

            // Actualizar las propiedades
            reservaExistente.ClienteId = reserva.ClienteId;
            reservaExistente.VehiculoId = reserva.VehiculoId;
            reservaExistente.FechaRecogida = reserva.FechaRecogida;
            reservaExistente.FechaDevolucion = reserva.FechaDevolucion;
            reservaExistente.Estado = reserva.Estado;
            reservaExistente.TotalPrecio = reserva.TotalPrecio;

            contexto.Reservas.Update(reservaExistente);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Reserva actualizada exitosamente.");
            return true;
        }

        // Eliminar una reserva por ID
        public async Task<bool> EliminarReserva(int reservaId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var reserva = await contexto.Reservas.FindAsync(reservaId);

            if (reserva == null)
            {
                _toastService.ShowError($"La reserva con ID {reservaId} no fue encontrada.");
                return false;
            }

            contexto.Reservas.Remove(reserva);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Reserva eliminada exitosamente.");
            return true;
        }

        // Cambiar el estado de la reserva
        public async Task<bool> CambiarEstadoReserva(int reservaId, string nuevoEstado)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var reserva = await contexto.Reservas.FindAsync(reservaId);

            if (reserva == null)
            {
                _toastService.ShowError($"La reserva con ID {reservaId} no fue encontrada.");
                return false;
            }

            reserva.Estado = nuevoEstado;
            contexto.Reservas.Update(reserva);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess($"El estado de la reserva ha sido actualizado a {nuevoEstado}.");
            return true;
        }

        // Buscar reservas por cliente
        public async Task<List<Reserva>> BuscarReservasPorCliente(int clienteId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .Where(r => r.ClienteId == clienteId)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener las reservas con estado pendiente
        public async Task<List<Reserva>> ObtenerReservasPendientes()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .Where(r => r.Estado == "Pendiente")
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
