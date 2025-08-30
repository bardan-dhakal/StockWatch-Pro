namespace api.Helpers
{
    public class StockQueryHelper
    {
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}