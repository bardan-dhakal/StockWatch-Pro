using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository(ApplicationDbContext applicationDbContext) : IPortfolioRepository
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

        public async Task<CreatePortfolioDto?> CreateAsync(Portfolio portfolio)
        {
            if (applicationDbContext.Portfolios.Any(p => p.AppUserId == portfolio.AppUserId && p.StockId == portfolio.StockId))
            {
                return null;
            }
            await applicationDbContext.Portfolios.AddAsync(portfolio);
            await applicationDbContext.SaveChangesAsync();
            return portfolio.ToCreatePortfolioDto();
        }

        public async Task<bool> DeleteAsync(Portfolio portfolio)
        {

            Portfolio? selectedPortfolio = await applicationDbContext
            .Portfolios
            .FirstOrDefaultAsync(s => s.AppUserId == portfolio.AppUserId && s.StockId == portfolio.StockId);

            if (selectedPortfolio == null)
            {
                return false;
            }

            applicationDbContext.Remove(selectedPortfolio);
            await applicationDbContext.SaveChangesAsync();
            return true;
        }

        public List<StockDto> GetStocks(AppUser appUser)
        {
            List<StockDto> portfolios = applicationDbContext.Portfolios
            .Where(p => p.AppUserId == appUser.Id)
            .Select(p => p.Stock.ToStockDto())
            .ToList();

            return portfolios;
        }
    }
}