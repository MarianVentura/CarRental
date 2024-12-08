using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using BlazorBootstrap;
using CarRental.Extensors;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Services
{
    public class MetodoPagoService
    {
        private readonly Contexto _contexto; // Cambiado de IDbContextFactory<Contexto> a Contexto
        private readonly ToastService _toastService;

        public MetodoPagoService(Contexto contexto, ToastService toastService) // Cambiado de dbFactory a contexto
        {
            _contexto = contexto;
            _toastService = toastService;
        }

        // Obtener todos los métodos de pago
        public async Task<List<MetodoPago>> ObtenerTodosLosMetodosPago()
        {
            return await _contexto.MetodosPago
                .Include(m => m.Reserva)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un método de pago por ID
        public async Task<MetodoPago?> ObtenerMetodoPagoPorId(int id)
        {
            var metodoPago = await _contexto.MetodosPago
                .Include(m => m.Reserva)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);

            if (metodoPago == null)
            {
                _toastService.ShowError($"El método de pago con ID {id} no fue encontrado.");
                return null;
            }

            return metodoPago;
        }

        // Validar un método de pago
        private bool ValidarMetodoPago(MetodoPago metodoPago)
        {
            if (metodoPago == null)
            {
                _toastService.ShowError("El método de pago no puede ser nulo.");
                return false;
            }

            var context = new ValidationContext(metodoPago, null, null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(metodoPago, context, validationResults, true))
            {
                foreach (var error in validationResults)
                {
                    _toastService.ShowError(error.ErrorMessage ?? "Error desconocido.");
                }
                return false;
            }

            return true;
        }

        // Agregar un nuevo método de pago
        public async Task<bool> AgregarMetodoPago(MetodoPago metodoPago)
        {
            if (!ValidarMetodoPago(metodoPago)) return false;

            _contexto.MetodosPago.Add(metodoPago);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Método de pago agregado exitosamente.");
            return true;
        }

        // Actualizar un método de pago existente
        public async Task<bool> ActualizarMetodoPago(MetodoPago metodoPago)
        {
            if (!ValidarMetodoPago(metodoPago)) return false;

            var metodoPagoExistente = await _contexto.MetodosPago.FindAsync(metodoPago.MetodoPagoId);

            if (metodoPagoExistente == null)
            {
                _toastService.ShowError($"El método de pago con ID {metodoPago.MetodoPagoId} no fue encontrado.");
                return false;
            }

            // Actualizar las propiedades del método de pago
            metodoPagoExistente.Tipo = metodoPago.Tipo;
            metodoPagoExistente.FechaTransaccion = metodoPago.FechaTransaccion;
            metodoPagoExistente.Monto = metodoPago.Monto;
            metodoPagoExistente.EstadoTransaccion = metodoPago.EstadoTransaccion;
            metodoPagoExistente.ProveedorPago = metodoPago.ProveedorPago;

            _contexto.MetodosPago.Update(metodoPagoExistente);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Método de pago actualizado exitosamente.");
            return true;
        }

        // Eliminar un método de pago por ID
        public async Task<bool> EliminarMetodoPago(int metodoPagoId)
        {
            var metodoPago = await _contexto.MetodosPago.FindAsync(metodoPagoId);

            if (metodoPago == null)
            {
                _toastService.ShowError($"El método de pago con ID {metodoPagoId} no fue encontrado.");
                return false;
            }

            _contexto.MetodosPago.Remove(metodoPago);
            await _contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Método de pago eliminado exitosamente.");
            return true;
        }

        // Buscar métodos de pago por reserva
        public async Task<List<MetodoPago>> BuscarMetodosPagoPorReserva(int reservaId)
        {
            return await _contexto.MetodosPago
                .Where(m => m.ReservaId == reservaId)
                .Include(m => m.Reserva)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener métodos de pago con estado pendiente
        public async Task<List<MetodoPago>> ObtenerMetodosPagoPendientes()
        {
            return await _contexto.MetodosPago
                .Where(m => m.EstadoTransaccion == "Pendiente")
                .Include(m => m.Reserva)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
