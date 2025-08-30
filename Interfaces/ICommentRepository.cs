using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetCommentList();
        Task<CommentDto?> GetByIdAsync(int id);
        Task<Comment> Create(int stockId,CreateCommentDto createCommentDto);
        Task<GenericResponse<CommentDto?>> Update(int commentId,UpdateCommentDto updateCommentDto);
        Task<GenericResponse<CommentDto?>> Delete(int commentId);
    }
}