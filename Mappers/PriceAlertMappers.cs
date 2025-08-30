using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class PriceAlertMappers
    {
        public static PriceAlertDto ToPriceAlertDto(this PriceAlert priceAlert)
        {
            return new PriceAlertDto
            {
                Id = priceAlert.Id,
                StockId = priceAlert.StockId,
                StockSymbol = priceAlert.Stock.Symbol,
                CompanyName = priceAlert.Stock.CompanyName,
                TargetPrice = priceAlert.TargetPrice,
                AlertType = priceAlert.AlertType,
                IsActive = priceAlert.IsActive,
                CreatedAt = priceAlert.CreatedAt,
                TriggeredAt = priceAlert.TriggeredAt
            };
        }
    }
}
