using api.Data;
using api.Dtos;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(ApplicationDbContext applicationDbContext) : IStockRepository
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

        public async Task<StockDto> Create(CreateStockRequestDto createStockRequestDto)
        {
            var stock = createStockRequestDto.ToCreateStockDto();
            await applicationDbContext.AddAsync(stock);
            await applicationDbContext.SaveChangesAsync();
            return stock.ToStockDto();
        }

        public async Task<bool> Delete(int id)
        {
            Stock? stock =  await applicationDbContext
            .Stocks
            .FirstOrDefaultAsync(s => s.Id == id);

            if(stock is null){
                return false;
            }

            applicationDbContext.Remove(stock);
            await applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<StockDto>> GetAll(StockQueryHelper stockQueryHelper)
        {
            var stockQuery = applicationDbContext.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrEmpty(stockQueryHelper.CompanyName))
            {
                stockQuery = stockQuery.Where(s => s.CompanyName.Contains(stockQueryHelper.CompanyName));
            }

            if (!string.IsNullOrEmpty(stockQueryHelper.Symbol))
            {
                stockQuery = stockQuery.Where(s => s.Symbol.Contains(stockQueryHelper.Symbol));
            }

            var skipNumber = (stockQueryHelper.PageNumber - 1) * stockQueryHelper.PageSize;

            var stockList = await stockQuery
            .Skip(skipNumber)
            .Take(stockQueryHelper.PageSize)
            .Select(s => s.ToStockDto())
            .ToListAsync();
            return stockList;
        }

        public async Task<StockDto?> GetById(int id)
        {
            var stock = await applicationDbContext.Stocks
            .Include(s => s.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);
            if(stock is null){
                return null;
            }
            return stock.ToStockDto();
        }

        public async Task<bool> IsStockExist(int stockId)
        {
            return await applicationDbContext.Stocks.AnyAsync(s => s.Id == stockId);
        }

        public async Task<StockDto?> Update(int id,UpdateStockRequestDto updateStockRequestDto)
        {
            Stock? stockModel = await applicationDbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel is null){
                return null;
            }

            stockModel.Symbol = updateStockRequestDto.Symbol;
            stockModel.CompanyName = updateStockRequestDto.CompanyName;
            stockModel.Purchase = updateStockRequestDto.Purchase;
            stockModel.LastDiv = updateStockRequestDto.LastDiv;
            stockModel.Industry = updateStockRequestDto.Industry;
            stockModel.MarketCap = updateStockRequestDto.MarketCap;

            applicationDbContext.Update(stockModel);
            await applicationDbContext.SaveChangesAsync();
            return stockModel.ToStockDto();
        }
    }
}