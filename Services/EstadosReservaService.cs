using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using BlazorBootstrap;
using CarRental.Extensors;

namespace CarRental.Services
{
    public class EstadoReservasService
    {
        private readonly Contexto _contexto;
        private readonly ToastService _toastService;

        public EstadoReservasService(Contexto contexto, ToastService toastService)
        {
            _contexto = contexto;
            _toastService = toastService;
        }

        // Obtener todos los estados de reserva
        public async Task<List<EstadoReservas>> ObtenerTodosLosEstados()
        {
            return await _contexto.EstadoReservas
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un estado de reserva por ID
        public async Task<EstadoReservas?> ObtenerEstadoPorId(int id)
        {
            var estado = await _contexto.EstadoReservas
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EstadoId == id);

            if (estado == null)
            {
                _toastService.ShowError($"El estado de reserva con ID {id} no fue encontrado.");
                return null;
            }

            return estado;
        }

        // Agregar un nuevo estado de reserva
        public async Task<bool> AgregarEstado(EstadoReservas estado)
        {
            if (estado == null)
            {
                _toastService.ShowError("El estado de reserva no puede ser nulo.");
                return false;
            }

            // Validar nombre duplicado
            var existe = await _contexto.EstadoReservas
                .AnyAsync(e => e.EstadoName.ToLower() == estado.EstadoName.ToLower());
            if (existe)
            {
                _toastService.ShowError("Ya existe un estado de reserva con el mismo nombre.");
                return false;
            }

            _contexto.EstadoReservas.Add(estado);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Estado de reserva agregado exitosamente.");
            return true;
        }

        // Actualizar un estado de reserva existente
        public async Task<bool> ActualizarEstado(EstadoReservas estado)
        {
            if (estado == null)
            {
                _toastService.ShowError("El estado de reserva no puede ser nulo.");
                return false;
            }

            var estadoExistente = await _contexto.EstadoReservas.FindAsync(estado.EstadoId);

            if (estadoExistente == null)
            {
                _toastService.ShowError($"El estado de reserva con ID {estado.EstadoId} no fue encontrado.");
                return false;
            }

            // Validar nombre duplicado
            var existe = await _contexto.EstadoReservas
                .AnyAsync(e => e.EstadoId != estado.EstadoId && e.EstadoName.ToLower() == estado.EstadoName.ToLower());
            if (existe)
            {
                _toastService.ShowError("Ya existe otro estado de reserva con el mismo nombre.");
                return false;
            }

            // Actualizar propiedades
            estadoExistente.EstadoName = estado.EstadoName;

            _contexto.EstadoReservas.Update(estadoExistente);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Estado de reserva actualizado exitosamente.");
            return true;
        }

        // Eliminar un estado de reserva por ID
        public async Task<bool> EliminarEstado(int id)
        {
            var estado = await _contexto.EstadoReservas.FindAsync(id);

            if (estado == null)
            {
                _toastService.ShowError($"El estado de reserva con ID {id} no fue encontrado.");
                return false;
            }

            _contexto.EstadoReservas.Remove(estado);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Estado de reserva eliminado exitosamente.");
            return true;
        }

        // Comprobar si existe un estado de reserva con el mismo nombre (excluyendo un ID)
        public async Task<bool> ExisteEstadoConNombre(int id, string? nombre)
        {
            if (string.IsNullOrEmpty(nombre))
                return false;

            return await _contexto.EstadoReservas
                .AnyAsync(e => e.EstadoId != id && e.EstadoName.ToLower() == nombre.ToLower());
        }
    }
}
