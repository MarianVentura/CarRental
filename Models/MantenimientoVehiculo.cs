using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarRental.Models;



public class MantenimientoVehiculo
{
    [Key]
    public int MantenimientoId { get; set; }

    [Required(ErrorMessage = "El vehículo es obligatorio.")]
    [ForeignKey("Vehiculo")]
    public int VehiculoId { get; set; }

    [Required(ErrorMessage = "La fecha de mantenimiento es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaMantenimiento { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s,.]+$", ErrorMessage = "La descripción solo puede contener letras, números, espacios y caracteres básicos.")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El costo es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser un valor positivo.")]
    public decimal Costo { get; set; }
}
