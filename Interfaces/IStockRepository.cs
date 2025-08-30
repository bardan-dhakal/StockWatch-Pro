using api.Dtos;
using api.Helpers;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAll(StockQueryHelper stockQueryHelper);
        Task<StockDto?> GetById(int id);
        Task<StockDto> Create(CreateStockRequestDto requestDto);
        Task<StockDto?> Update(int id,UpdateStockRequestDto updateStockRequestDto);
        Task<bool> Delete(int id);
        Task<bool> IsStockExist(int stockId);

    }
}