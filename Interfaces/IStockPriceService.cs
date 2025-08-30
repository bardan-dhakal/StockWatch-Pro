namespace api.Interfaces
{
    public interface IStockPriceService
    {
        Task<decimal?> GetCurrentPriceAsync(string symbol);
        Task<Dictionary<string, decimal>> GetMultiplePricesAsync(List<string> symbols);
        Task<bool> IsPriceAlertTriggeredAsync(string symbol, decimal targetPrice, AlertType alertType);
    }
}
