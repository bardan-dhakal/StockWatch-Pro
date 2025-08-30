using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.Dtos
{
    public class CreatePriceAlertDto
    {
        [Required]
        public int StockId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Target price must be greater than 0")]
        public decimal TargetPrice { get; set; }
        
        [Required]
        public AlertType AlertType { get; set; }
    }
}
