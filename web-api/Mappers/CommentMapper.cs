using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.DTOs.Comment;
using web_api.Models;

namespace web_api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment){
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

           public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId){
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}