using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Models.Interfaces
{
    public interface ICategory
    {
        Task AddCategory(Category newCategory);

        Task UpdateCategory(Category updateCategory);

        Task DeleteCategory(int id);

        Task<Category> GetCategory(int? id);

        Task<List<Category>> GetCategories(string userId);

        Task<List<Note>> GetAllNotesFromCategory(int? id);
    }
}
