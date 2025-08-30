using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PriceAlertRepository : IPriceAlertRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceAlertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceAlertDto>> GetUserAlertsAsync(string userId)
        {
            var alerts = await _context.PriceAlerts
                .Include(pa => pa.Stock)
                .Where(pa => pa.AppUserId == userId)
                .OrderByDescending(pa => pa.CreatedAt)
                .ToListAsync();

            return alerts.Select(pa => pa.ToPriceAlertDto()).ToList();
        }

        public async Task<PriceAlertDto?> GetByIdAsync(int id, string userId)
        {
            var alert = await _context.PriceAlerts
                .Include(pa => pa.Stock)
                .FirstOrDefaultAsync(pa => pa.Id == id && pa.AppUserId == userId);

            return alert?.ToPriceAlertDto();
        }

        public async Task<PriceAlertDto> CreateAsync(CreatePriceAlertDto createDto, string userId)
        {
            var stock = await _context.Stocks.FindAsync(createDto.StockId);
            if (stock == null)
                throw new ArgumentException("Stock not found");

            var priceAlert = new PriceAlert
            {
                AppUserId = userId,
                StockId = createDto.StockId,
                TargetPrice = createDto.TargetPrice,
                AlertType = createDto.AlertType,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.PriceAlerts.Add(priceAlert);
            await _context.SaveChangesAsync();

            // Reload with stock data for mapping
            await _context.Entry(priceAlert).Reference(pa => pa.Stock).LoadAsync();
            
            return priceAlert.ToPriceAlertDto();
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var alert = await _context.PriceAlerts
                .FirstOrDefaultAsync(pa => pa.Id == id && pa.AppUserId == userId);

            if (alert == null)
                return false;

            _context.PriceAlerts.Remove(alert);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(int id, string userId)
        {
            var alert = await _context.PriceAlerts
                .FirstOrDefaultAsync(pa => pa.Id == id && pa.AppUserId == userId);

            if (alert == null)
                return false;

            alert.IsActive = !alert.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PriceAlert>> GetActiveAlertsForStockAsync(int stockId)
        {
            return await _context.PriceAlerts
                .Include(pa => pa.AppUser)
                .Include(pa => pa.Stock)
                .Where(pa => pa.StockId == stockId && pa.IsActive)
                .ToListAsync();
        }

        public async Task MarkAlertAsTriggeredAsync(int alertId)
        {
            var alert = await _context.PriceAlerts.FindAsync(alertId);
            if (alert != null)
            {
                alert.TriggeredAt = DateTime.UtcNow;
                alert.IsActive = false; // Deactivate after triggering
                await _context.SaveChangesAsync();
            }
        }
    }
}
