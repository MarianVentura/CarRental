using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public class Seguros
{
    [Key]
    public int SeguroId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Nombre { get; set; } // Ejemplo: "Cobertura completa", "Daños por accidente"

    public string? Descripcion { get; set; } // Detalles sobre qué incluye el seguro

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser un valor positivo.")]
    public decimal Costo { get; set; } // Costo del seguro por día o por contrato

    public string? Cobertura { get; set; } // Ejemplo: "Daños por accidente", "Robo", "Asistencia en carretera"
    public string? Proveedor { get; set; } // Empresa aseguradora
}
