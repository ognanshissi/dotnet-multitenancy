using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateAsync(string name);
        Task<Category> GetByIdAsync(int id);
        Task<IReadOnlyList<Category>> GetAllAsync();
    }
}