
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {
        private readonly ICommentRepository commentRepository = commentRepository;
        private readonly IStockRepository stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CommentDto>? comments = await commentRepository.GetCommentList();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            CommentDto? comment = await commentRepository.GetByIdAsync(id);
            if (comment is null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!await stockRepository.IsStockExist(stockId))
            {
                return NotFound();
            }
            var newComment = await commentRepository.Create(stockId, createCommentDto);
            return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment.ToCommentDto());
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update([FromRoute] int commentId, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var response = await commentRepository.Update(commentId, updateCommentDto);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            var response = await commentRepository.Delete(commentId);
            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

    }
}