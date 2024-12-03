using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;

public class Reserva
{
    [Key]
    public int ReservaId { get; set; }

    [Required]
    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    [Required]
    [ForeignKey("Vehiculo")]
    public int VehiculoId { get; set; }
    public Vehiculo Vehiculo { get; set; }

    [Required(ErrorMessage = "La fecha de recogida es obligatoria.")]
    public DateTime FechaRecogida { get; set; }

    [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
    public DateTime FechaDevolucion { get; set; }

    [Required(ErrorMessage = "El estado de la reserva es obligatorio.")]
    public EstadoReserva Estado { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0.")]
    public decimal TotalPrecio { get; set; }

    // Enum para el estado de la reserva
    public enum EstadoReserva
    {
        Confirmada,
        Cancelada,
        Pendiente
    }
}
