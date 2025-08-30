using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class PortfolioMappers
    {
        public static CreatePortfolioDto ToCreatePortfolioDto(this Portfolio portfolio)
        {
            return new CreatePortfolioDto
            {
                StockId = portfolio.StockId,
                AppUserId = portfolio.AppUserId
            };
        }
    }
}