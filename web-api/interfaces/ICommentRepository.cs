using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.DTOs.Comment;
using web_api.Models;

namespace web_api.interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment> GetByIdAsync(int id);

        Task<Comment> CreateAsync(Comment comment);

        Task<Comment> UpdateAsync(int id, Comment comment);

        Task<Comment?> DeleteAsync(int id);
    }
}