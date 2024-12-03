using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;


public class Vehiculo
{
    [Key]
    public int VehiculoId { get; set; }

    [Required(ErrorMessage = "La marca es obligatoria.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "La marca solo puede contener letras y espacios.")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "El modelo es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El modelo solo puede contener letras, números y espacios.")]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "El año es obligatorio.")]
    [Range(1900, 2100, ErrorMessage = "El año debe estar entre 1900 y 2100.")]
    public int Año { get; set; }

    [Required(ErrorMessage = "El precio por día es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public decimal PrecioPorDia { get; set; }

    [Required(ErrorMessage = "El estado de disponibilidad es obligatorio.")]
    public bool Disponible { get; set; }

    [Required(ErrorMessage = "La imagen es obligatoria.")]
    [Url(ErrorMessage = "Debe ser una URL válida.")]
    public string Imagen { get; set; }

    [Required(ErrorMessage = "El tipo de combustible es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El tipo de combustible solo puede contener letras y espacios.")]
    public string Combustible { get; set; }

    [Required(ErrorMessage = "El tipo de transmisión es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El tipo de transmisión solo puede contener letras y espacios.")]
    public string TipoTransmision { get; set; }

    [Required(ErrorMessage = "El color es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El color solo puede contener letras y espacios.")]
    public string Color { get; set; }
}
