using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarRental.Models;

public class MantenimientoVehiculo
{
    [Key]
    public int MantenimientoId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int VehiculoId { get; set; }

    [ForeignKey("VehiculoId")]
    public Vehiculos? Vehiculo { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public DateTime FechaMantenimiento { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string Descripcion { get; set; } // Descripción del mantenimiento realizado

    public decimal Costo { get; set; } // Costo del mantenimiento
}
