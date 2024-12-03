using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;

public class MetodoPago
{
    [Key]
    public int MetodoPagoId { get; set; }

    [Required(ErrorMessage = "El tipo de método de pago es obligatorio.")]
    public TipoMetodoPago Tipo { get; set; }

    public DateTime FechaTransaccion { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Monto { get; set; }

    [Required]
    [ForeignKey("Reserva")]
    public int ReservaId { get; set; }
    public Reserva Reserva { get; set; }

    [Required(ErrorMessage = "El estado de la transacción es obligatorio.")]
    public TransaccionEstado EstadoTransaccion { get; set; }

    public string ProveedorPago { get; set; }

    // Enum para el tipo de método de pago
    public enum TipoMetodoPago
    {
        TarjetaDeCredito,
        TransferenciaBancaria
    }

    // Enum para el estado de la transacción
    public enum TransaccionEstado
    {
        Aprobada,
        Rechazada,
        Pendiente
    }
}
