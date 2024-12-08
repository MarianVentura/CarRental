using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;

public class MetodoPago
{
    [Key]
    public int MetodoPagoId { get; set; }

    [Required(ErrorMessage = "El tipo de pago es obligatorio.")]
    public string? Tipo { get; set; }

    [Required(ErrorMessage = "La fecha de transacción es obligatoria.")]
    public DateTime FechaTransaccion { get; set; }

    [Required(ErrorMessage = "El monto es obligatorio.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "La reserva es obligatoria.")]
    [ForeignKey("Reserva")]
    public int ReservaId { get; set; }
    public virtual Reserva? Reserva { get; set; }

    [Required(ErrorMessage = "El estado de la transacción es obligatorio.")]
    public string? EstadoTransaccion { get; set; }

    [Required(ErrorMessage = "El proveedor del pago es obligatorio.")]
    public string? ProveedorPago { get; set; }
}