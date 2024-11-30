using System.ComponentModel.DataAnnotations;


namespace CarRental.Models;

public class Categorias
{
    [Key]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? ImagenURL { get; set; } // Ejemplo: una imagen para identificar visualmente la categoría
}