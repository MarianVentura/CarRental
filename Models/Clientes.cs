using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
  
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "La identificación es obligatoria.")]
    
    public string? Identificacion { get; set; }
}
