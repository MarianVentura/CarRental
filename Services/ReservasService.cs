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
        private readonly Contexto _contexto;
        private readonly ToastService _toastService;

        public ReservasService(Contexto contexto, ToastService toastService)
        {
            _contexto = contexto;
            _toastService = toastService;
        }

        // Obtener todas las reservas
        public async Task<List<Reserva>> ObtenerTodasLasReservas()
        {
            return await _contexto.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener una reserva por su ID
        public async Task<Reserva?> ObtenerReservaPorId(int id)
        {
            var reserva = await _contexto.Reservas
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

            // Validar existencia de Cliente y Vehículo
            var clienteExiste = await _contexto.Clientes.AnyAsync(c => c.ClienteId == reserva.ClienteId);
            var vehiculoExiste = await _contexto.Vehiculos.AnyAsync(v => v.VehiculoId == reserva.VehiculoId);

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

            _contexto.Reservas.Add(reserva);
            await _contexto.SaveChangesAsync();

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

            var reservaExistente = await _contexto.Reservas.FindAsync(reserva.ReservaId);

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

            _contexto.Reservas.Update(reservaExistente);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Reserva actualizada exitosamente.");
            return true;
        }

        // Eliminar una reserva por ID
        public async Task<bool> EliminarReserva(int reservaId)
        {
            var reserva = await _contexto.Reservas.FindAsync(reservaId);

            if (reserva == null)
            {
                _toastService.ShowError($"La reserva con ID {reservaId} no fue encontrada.");
                return false;
            }

            _contexto.Reservas.Remove(reserva);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Reserva eliminada exitosamente.");
            return true;
        }

        // Cambiar el estado de la reserva
        public async Task<bool> CambiarEstadoReserva(int reservaId, string nuevoEstado)
        {
            var reserva = await _contexto.Reservas.FindAsync(reservaId);

            if (reserva == null)
            {
                _toastService.ShowError($"La reserva con ID {reservaId} no fue encontrada.");
                return false;
            }

            reserva.Estado = nuevoEstado;
            _contexto.Reservas.Update(reserva);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess($"El estado de la reserva ha sido actualizado a {nuevoEstado}.");
            return true;
        }

        // Buscar reservas por cliente
        public async Task<List<Reserva>> BuscarReservasPorCliente(int clienteId)
        {
            return await _contexto.Reservas
                .Where(r => r.ClienteId == clienteId)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener las reservas con estado pendiente
        public async Task<List<Reserva>> ObtenerReservasPendientes()
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == "Pendiente")
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener todas las reservas activas
        public async Task<List<Reserva>> ObtenerReservasActivas()
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == "Activa")  // Ajusta la condición de estado según tu modelo
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener reservas activas por cliente
        public async Task<List<Reserva>> ObtenerReservasActivasPorCliente(int clienteId)
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == "Activa" && r.ClienteId == clienteId)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener reservas pendientes por cliente
        public async Task<List<Reserva>> ObtenerReservasPendientesPorCliente(int clienteId)
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == "Pendiente" && r.ClienteId == clienteId)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener reservas activas por vehículo
        public async Task<List<Reserva>> ObtenerReservasActivasPorVehiculo(int vehiculoId)
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == "Activa" && r.VehiculoId == vehiculoId)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener reservas pendientes por vehículo
        public async Task<List<Reserva>> ObtenerReservasPendientesPorVehiculo(int vehiculoId)
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == "Pendiente" && r.VehiculoId == vehiculoId)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener reservas pendientes por estado
        public async Task<List<Reserva>> ObtenerReservasPendientesPorEstado(string estado)
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == estado)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener reservas activas por estado
        public async Task<List<Reserva>> ObtenerReservasActivasPorEstado(string estado)
        {
            return await _contexto.Reservas
                .Where(r => r.Estado == estado)
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
