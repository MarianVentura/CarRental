using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Este campo es oblogatorio.")]
    [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Direccion { get; set; }
    public string? Identificacion { get; set; } 

    
}