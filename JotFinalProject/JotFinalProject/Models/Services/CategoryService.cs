using JotFinalProject.Data;
using JotFinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models.Services
{
    public class CategoryService : ICategory
    {
        private JotDbContext _context;

        public CategoryService(JotDbContext context)
        {
            _context = context;
        }

        public async Task AddCategory(Category newCategory)
        {
            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            Category category = await GetCategory(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAllNotesFromCategory(int? id)
        {
            var result = await _context.Notes.Where(note => note.ID == id).ToListAsync();
            return result;
        }

        public async Task<List<Category>> GetCategories(string userId)
        {
            return await _context.Category.Where(cat => cat.UserID == userId).ToListAsync();
        }

        public async Task<Category> GetCategory(int? id)
        {
            return await _context.Category.FirstOrDefaultAsync(cat => cat.ID == id);
        }

        public async Task UpdateCategory(Category updateCategory)
        {
            _context.Category.Update(updateCategory);
            await _context.SaveChangesAsync();
        }
    }
}
