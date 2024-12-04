using CarRental.Models;
using System.ComponentModel.DataAnnotations;

public class Vehiculo
{
    [Key]
    public int VehiculoId { get; set; }

    [Required(ErrorMessage = "La marca es obligatoria.")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "El modelo es obligatorio.")]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "El año es obligatorio.")]
    [Range(1900, 2100, ErrorMessage = "El año debe estar entre 1900 y 2100.")]
    public int Año { get; set; }

    [Required(ErrorMessage = "El precio por día es obligatorio.")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal PrecioPorDia { get; set; }

    public bool Disponible { get; set; } = true;

    public string Imagen { get; set; }

    [Required(ErrorMessage = "El tipo de combustible es obligatorio.")]
    public TipoCombustible Combustible { get; set; }

    [Required(ErrorMessage = "El tipo de transmisión es obligatorio.")]
    public TipoTransmision Transmision { get; set; }

    [Required(ErrorMessage = "El color es obligatorio.")]
    public string Color { get; set; }
}
