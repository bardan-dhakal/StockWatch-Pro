using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Symbol required minimum 1 character long")]
        [MaxLength(10, ErrorMessage = "Symbol required maximum 10 character long")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Company name cannot be empty")]
        [MaxLength(100, ErrorMessage = "Company name required maximum 100 character long")]
        public string CompanyName { get; set; } = string.Empty;

        [Range(0, 1000000, ErrorMessage = "Purchase must be between 0 and 1,000,000")]
        public decimal Purchase { get; set; }

        [Range(0, 100, ErrorMessage = "LastDiv must be between 0 and 100")]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Industry required minimum 1 character long")]
        [MaxLength(10, ErrorMessage = "Industry required maximum 10 character long")]
        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
    }

}