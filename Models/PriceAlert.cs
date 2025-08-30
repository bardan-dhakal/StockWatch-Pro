using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class PriceAlert
    {
        public int Id { get; set; }
        
        [Required]
        public string AppUserId { get; set; } = string.Empty;
        
        [Required]
        public int StockId { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetPrice { get; set; }
        
        [Required]
        public AlertType AlertType { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? TriggeredAt { get; set; }
        
        // Navigation properties
        public AppUser AppUser { get; set; } = null!;
        public Stock Stock { get; set; } = null!;
    }
    
    public enum AlertType
    {
        PriceAbove,
        PriceBelow
    }
}
