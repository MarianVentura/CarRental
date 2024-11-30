using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models;

public class Vehiculos
{
    [Key]
    public int VehiculosId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Marca { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Modelo { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public decimal PrecioPorDia { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Categoria { get; set; }

    public bool Disponible { get; set; } = true;

    public string ImagenURL { get; set; }

    public string? HistorialMantenimiento { get; set; }

    // Relación con Combustible
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int CombustibleId { get; set; }

    [ForeignKey("CombustibleId")]
    public Combustible? Combustible { get; set; }

    // Relación con Seguros
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int SeguroId { get; set; }

    [ForeignKey("SeguroId")]
    public Seguros? Seguro { get; set; }

    // Nuevos campos adicionales
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int Año { get; set; } // Año de fabricación

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Transmision { get; set; } // Manual o Automática

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int Puertas { get; set; } // Cantidad de puertas

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int Asientos { get; set; } // Cantidad de asientos

    public string? Color { get; set; } // Color del vehículo

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? NumeroPlaca { get; set; } // Número de placa o matrícula

    public decimal Kilometraje { get; set; } // Kilometraje actual

    public string? Observaciones { get; set; } // Notas adicionales sobre el vehículo
}
