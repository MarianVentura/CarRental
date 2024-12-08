using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;

public class Reserva
{
    public enum EstadoReserva
    {
        Pendiente,
        Confirmada,
        Cancelada,
        Completada
    }

    [Key]
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio.")]
    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    [Required(ErrorMessage = "El vehículo es obligatorio.")]
    [ForeignKey("Vehiculo")]
    public int VehiculoId { get; set; }
    public virtual Vehiculo Vehiculo { get; set; }

    [Required(ErrorMessage = "La fecha de recogida es obligatoria.")]
    public DateTime FechaRecogida { get; set; }

    [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
    public DateTime FechaDevolucion { get; set; }

    [Required(ErrorMessage = "El precio total es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio total debe ser un valor positivo.")]
    public decimal TotalPrecio { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public EstadoReserva Estado { get; set; } // Cambiar a tipo enum EstadoReserva
}