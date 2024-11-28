using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CarRental.Models;

public class Reservas
{
    [Key]
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int ClienteId { get; set; }

    public Clientes? Cliente { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int VehiculoId { get; set; }

    public Vehiculos? Vehiculo { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public DateTime FechaRecogida { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public DateTime FechaDevolucion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public decimal TotalPrecio { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Estado { get; set; }

    public string? LugarRecogida { get; set; }
    public string? LugarEntrega { get; set; }
    public string? Comentarios { get; set; }

    public virtual MetodoPago? MetodoPago { get; set; }
}