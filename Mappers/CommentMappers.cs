using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment){
            return new CommentDto{
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedTime = comment.CreatedTime,
                StockId = comment.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto comment,int stockId){
            return new Comment{
                StockId = stockId,
                Title = comment.Title,
                Content = comment.Content,
                CreatedTime = comment.CreatedTime,
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentDto comment){
            return new Comment{
                Title = comment.Title,
                Content = comment.Content,
            };
        }
    }
}