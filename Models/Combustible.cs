using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public class Combustible
{
    [Key]
    public int CombustibleId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Tipo { get; set; } // Ejemplo: "Gasolina", "Diesel", "Eléctrico"

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Politica { get; set; } // Ejemplo: "Lleno/Lleno", "Lleno/Vacío"

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El costo por litro debe ser un valor positivo.")]
    public decimal CostoPorLitro { get; set; }

    public string? Descripcion { get; set; } // Información adicional sobre la política o tipo de combustible
}
