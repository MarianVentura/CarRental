using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;


public class Reserva
{
    [Key]
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio.")]
    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El vehículo es obligatorio.")]
    [ForeignKey("Vehiculo")]
    public int VehiculoId { get; set; }

    [Required(ErrorMessage = "La fecha de recogida es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaRecogida { get; set; }

    [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaDevolucion { get; set; }

    [Required(ErrorMessage = "El precio total es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio total debe ser un valor positivo.")]
    public decimal TotalPrecio { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression(@"^(Confirmada|Cancelada|Pendiente)$", ErrorMessage = "El estado debe ser Confirmada, Cancelada o Pendiente.")]
    public string Estado { get; set; }
}
