using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^\+?[0-9\s-]{7,15}$", ErrorMessage = "El teléfono debe ser válido y puede incluir el prefijo internacional.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s,.#-]+$", ErrorMessage = "La dirección solo puede contener letras, números, espacios y caracteres básicos como coma, punto o guión.")]
    [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "La identificación es obligatoria.")]
    [RegularExpression(@"^[0-9a-zA-Z-]+$", ErrorMessage = "La identificación solo puede contener letras, números y guiones.")]
    [StringLength(20, ErrorMessage = "La identificación no puede exceder los 20 caracteres.")]
    public string? Identificacion { get; set; }
}
