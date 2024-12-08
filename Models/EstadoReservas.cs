using System.ComponentModel.DataAnnotations;


namespace CarRental.Models
{
    public class EstadoReservas
    {
        [Key]
        public int EstadoId { get; set; }

        public string? EstadoName { get; set; }
    }
}
