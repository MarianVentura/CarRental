using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;



public class MetodoPago
{
    [Key]
    public int MetodoPagoId { get; set; }

    [Required(ErrorMessage = "El tipo de pago es obligatorio.")]
    [RegularExpression(@"^(Tarjeta de crédito|Transferencia Bancaria)$", ErrorMessage = "El tipo debe ser Tarjeta de crédito o Transferencia Bancaria.")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "La fecha de transacción es obligatoria.")]
    [DataType(DataType.DateTime)]
    public DateTime FechaTransaccion { get; set; }

    [Required(ErrorMessage = "El monto es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un valor positivo.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "La reserva es obligatoria.")]
    [ForeignKey("Reserva")]
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "El estado de la transacción es obligatorio.")]
    [RegularExpression(@"^(Aprobada|Rechazada|Pendiente)$", ErrorMessage = "El estado debe ser Aprobada, Rechazada o Pendiente.")]
    public string EstadoTransaccion { get; set; }

}
