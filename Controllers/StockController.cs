using api.Data;
using api.Dtos;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController(ApplicationDbContext applicationDbContext, IStockRepository stockRepository) : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IStockRepository stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryHelper stockQueryHelper)
        {

            return Ok(await stockRepository.GetAll(stockQueryHelper));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stockDto = await stockRepository.GetById(id);
            return stockDto is null ? NotFound() : Ok(stockDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto requestDto)
        {
            var stock = await stockRepository.Create(requestDto);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await stockRepository.Update(id, updateDto);
            return stockModel is null ? NotFound() : Ok(stockModel);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool isDeleted = await stockRepository.Delete(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}