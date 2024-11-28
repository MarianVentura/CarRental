using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CarRental.Models
{
    public class Reservas
    {
        [Key]
        public int ReservaId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Clientes? Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int VehiculoId { get; set; }

        [ForeignKey("VehiculosId")]
        public Vehiculos? Vehiculo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime FechaRecogida { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime FechaDevolucion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public decimal TotalPrecio { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string? Estado { get; set; } // Ejemplo: "Pendiente", "Confirmada", "Cancelada"

        public string? LugarRecogida { get; set; }
        public string? LugarEntrega { get; set; }
        public string? Comentarios { get; set; }

        public int? MetodoPagoId { get; set; }

        [ForeignKey("MetodoPagoId")]
        public MetodoPago? MetodoPago { get; set; }
    }
}