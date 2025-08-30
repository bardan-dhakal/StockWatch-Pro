using api.Interfaces;
using api.Models;
using System.Text.Json;

namespace api.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StockPriceService> _logger;

        public StockPriceService(HttpClient httpClient, ILogger<StockPriceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<decimal?> GetCurrentPriceAsync(string symbol)
        {
            try
            {
                // Using a free stock API (Alpha Vantage or similar)
                // For demo purposes, we'll use a mock implementation
                // In production, you'd use a real API like Alpha Vantage, Yahoo Finance, etc.
                
                var response = await _httpClient.GetAsync($"https://query1.finance.yahoo.com/v8/finance/chart/{symbol}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var price = ParseYahooFinanceResponse(content);
                    return price;
                }
                
                // Fallback to mock data for demo
                return GetMockPrice(symbol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching price for symbol {Symbol}", symbol);
                return GetMockPrice(symbol);
            }
        }

        public async Task<Dictionary<string, decimal>> GetMultiplePricesAsync(List<string> symbols)
        {
            var prices = new Dictionary<string, decimal>();
            
            foreach (var symbol in symbols)
            {
                var price = await GetCurrentPriceAsync(symbol);
                if (price.HasValue)
                {
                    prices[symbol] = price.Value;
                }
            }
            
            return prices;
        }

        public async Task<bool> IsPriceAlertTriggeredAsync(string symbol, decimal targetPrice, AlertType alertType)
        {
            var currentPrice = await GetCurrentPriceAsync(symbol);
            
            if (!currentPrice.HasValue)
                return false;

            return alertType switch
            {
                AlertType.PriceAbove => currentPrice.Value >= targetPrice,
                AlertType.PriceBelow => currentPrice.Value <= targetPrice,
                _ => false
            };
        }

        private decimal? ParseYahooFinanceResponse(string jsonResponse)
        {
            try
            {
                using var document = JsonDocument.Parse(jsonResponse);
                var root = document.RootElement;
                
                if (root.TryGetProperty("chart", out var chart) &&
                    chart.TryGetProperty("result", out var result) &&
                    result.GetArrayLength() > 0)
                {
                    var firstResult = result[0];
                    if (firstResult.TryGetProperty("meta", out var meta) &&
                        meta.TryGetProperty("regularMarketPrice", out var priceElement))
                    {
                        return priceElement.GetDecimal();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing Yahoo Finance response");
            }
            
            return null;
        }

        private decimal GetMockPrice(string symbol)
        {
            // Mock prices for demo purposes
            var mockPrices = new Dictionary<string, decimal>
            {
                { "AAPL", 150.00m },
                { "GOOGL", 2800.00m },
                { "MSFT", 300.00m },
                { "AMZN", 3300.00m },
                { "TSLA", 700.00m },
                { "NVDA", 500.00m },
                { "META", 350.00m },
                { "NFLX", 500.00m }
            };

            if (mockPrices.TryGetValue(symbol.ToUpper(), out var price))
            {
                // Add some randomness to simulate price changes
                var random = new Random();
                var variation = (decimal)(random.NextDouble() - 0.5) * 10; // ±5 variation
                return price + variation;
            }

            return 100.00m; // Default price
        }
    }
}
