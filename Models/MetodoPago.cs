using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public class MetodoPago
{
    [Key]
    public int MetodoPagoId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Tipo { get; set; } //Ejemplo: "Tarjeta de Crédito", "Transferencia Bancaria" 

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public DateTime FechaTransaccion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int ReservaId { get; set; }

    public string EstadoTransaccion { get; set; } = "Pendiente"; // Estado de la transacción
    public string ProveedorPago { get; set; } // Ejemplo: "PayPal"

    public virtual Reservas Reserva { get; set; }
    public string? Moneda { get; set; } // Ejemplo: "USD", "EUR", "DOP"
}
