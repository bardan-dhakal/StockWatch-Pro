using api.Models;

namespace api.Dtos
{
    public class PriceAlertDto
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal TargetPrice { get; set; }
        public AlertType AlertType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? TriggeredAt { get; set; }
    }
}
