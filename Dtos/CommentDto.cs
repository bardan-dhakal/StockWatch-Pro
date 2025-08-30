using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int StockId { get; set; }
    }
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title required minimum 5 character long")]
        [MaxLength(180,ErrorMessage = "Title required maximum 180 character long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5,ErrorMessage = "Content required minimum 5 character long")]
        [MaxLength(180,ErrorMessage = "Content required maximum 180 character long")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title required minimum 5 character long")]
        [MaxLength(180,ErrorMessage = "Title required maximum 180 character long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5,ErrorMessage = "Content required minimum 5 character long")]
        [MaxLength(180,ErrorMessage = "Content required maximum 180 character long")]
        public string Content { get; set; } = string.Empty;
    }
}