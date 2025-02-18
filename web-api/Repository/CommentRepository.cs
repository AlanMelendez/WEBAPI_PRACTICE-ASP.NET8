using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.DTOs.Comment;
using web_api.interfaces;
using web_api.Models;

namespace web_api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
           var comments = await _context.Comments.ToListAsync();
              return comments;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            return comment;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> UpdateAsync(int id, Comment comment)
        {
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(commentToUpdate == null){
                return null;
            }

            commentToUpdate.Title = comment.Title;
            commentToUpdate.Content = comment.Content;

            await _context.SaveChangesAsync();

            return commentToUpdate;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if(commentModel == null){
                return null;
            }   

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }
    }
}