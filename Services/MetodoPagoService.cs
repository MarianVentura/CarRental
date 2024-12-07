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
        private readonly IDbContextFactory<Contexto> _dbFactory;
        private readonly ToastService _toastService;

        public MetodoPagoService(IDbContextFactory<Contexto> dbFactory, ToastService toastService)
        {
            _dbFactory = dbFactory;
            _toastService = toastService;
        }

        // Obtener todos los métodos de pago
        public async Task<List<MetodoPago>> ObtenerTodosLosMetodosPago()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.MetodosPago
                .Include(m => m.Reserva)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener un método de pago por ID
        public async Task<MetodoPago?> ObtenerMetodoPagoPorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var metodoPago = await contexto.MetodosPago
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

            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.MetodosPago.Add(metodoPago);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Método de pago agregado exitosamente.");
            return true;
        }

        // Actualizar un método de pago existente
        public async Task<bool> ActualizarMetodoPago(MetodoPago metodoPago)
        {
            if (!ValidarMetodoPago(metodoPago)) return false;

            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var metodoPagoExistente = await contexto.MetodosPago.FindAsync(metodoPago.MetodoPagoId);

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

            contexto.MetodosPago.Update(metodoPagoExistente);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Método de pago actualizado exitosamente.");
            return true;
        }

        // Eliminar un método de pago por ID
        public async Task<bool> EliminarMetodoPago(int metodoPagoId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var metodoPago = await contexto.MetodosPago.FindAsync(metodoPagoId);

            if (metodoPago == null)
            {
                _toastService.ShowError($"El método de pago con ID {metodoPagoId} no fue encontrado.");
                return false;
            }

            contexto.MetodosPago.Remove(metodoPago);
            await contexto.SaveChangesAsync();

            _toastService.ShowSuccess("Método de pago eliminado exitosamente.");
            return true;
        }

        // Buscar métodos de pago por reserva
        public async Task<List<MetodoPago>> BuscarMetodosPagoPorReserva(int reservaId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.MetodosPago
                .Where(m => m.ReservaId == reservaId)
                .Include(m => m.Reserva)
                .AsNoTracking()
                .ToListAsync();
        }

        // Obtener métodos de pago con estado pendiente
        public async Task<List<MetodoPago>> ObtenerMetodosPagoPendientes()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.MetodosPago
                .Where(m => m.EstadoTransaccion == "Pendiente")
                .Include(m => m.Reserva)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
