using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(string name)
        {
            var category = new Category(name);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
    }
}