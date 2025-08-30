using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface IPriceAlertRepository
    {
        Task<List<PriceAlertDto>> GetUserAlertsAsync(string userId);
        Task<PriceAlertDto?> GetByIdAsync(int id, string userId);
        Task<PriceAlertDto> CreateAsync(CreatePriceAlertDto createDto, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<bool> ToggleActiveAsync(int id, string userId);
        Task<List<PriceAlert>> GetActiveAlertsForStockAsync(int stockId);
        Task MarkAlertAsTriggeredAsync(int alertId);
    }
}
