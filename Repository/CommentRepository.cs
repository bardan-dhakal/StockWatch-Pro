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
    public class CommentRepository(ApplicationDbContext applicationDbContext) : ICommentRepository
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

        public async Task<Comment> Create(int stockId, CreateCommentDto createCommentDto)
        {
            var comment = createCommentDto.ToCommentFromCreate(stockId);
            await applicationDbContext.Comments.AddAsync(comment);
            await applicationDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<GenericResponse<CommentDto?>> Delete(int commentId)
        {
            var comment = await applicationDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if(comment is null){
                return GenericResponse<CommentDto?>.Failure("Not Found");
            }
            applicationDbContext.Remove(comment);
            await applicationDbContext.SaveChangesAsync();
            return GenericResponse<CommentDto?>.Success(comment.ToCommentDto());
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            var comment = await applicationDbContext.Comments.FindAsync(id);
            return comment?.ToCommentDto();
        }

        public async Task<List<CommentDto>> GetCommentList()
        {
            var comments = applicationDbContext.Comments.Select(s => s.ToCommentDto());
            return await comments.ToListAsync();
        }

        public async Task<GenericResponse<CommentDto?>> Update(int commentId, UpdateCommentDto updateCommentDto)
        {
            Comment? comment = await applicationDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if(comment is null){
                return GenericResponse<CommentDto?>.Failure("Not Found");
            }

            comment.Title = updateCommentDto.Title;
            comment.Content = updateCommentDto.Content;

            applicationDbContext.Update(comment);
            await applicationDbContext.SaveChangesAsync();
            return GenericResponse<CommentDto?>.Success(comment.ToCommentDto(),"Succesfull");
        }
    }
}