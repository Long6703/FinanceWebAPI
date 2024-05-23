using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);
            
            if(comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);

            return comment;
        
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(s => s.Id == id);

            if(commentToUpdate == null)
            {
                return null;
            }

            commentToUpdate.Title = comment.Title;
            commentToUpdate.Content = comment.Content;

            await _context.SaveChangesAsync();

            return commentToUpdate;
        }
    }
}