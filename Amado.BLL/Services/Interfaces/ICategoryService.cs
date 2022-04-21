

using Amado.Core.DTOs;
using Amado.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<string> AddAsync(CategoryDto entity);
        Task<string> DeleteAsync(int id);
        Task<string> RemoveFromDbAsync(int id);
        Task<string> UpdateAsync(CategoryDto entity);
        Task<List<CategoryDto>> GetAllAsync();
        Task<List<CategoryDto>> GetAllInActiveAsync();
        Task<List<CategoryDto>> GetAllActiveAsync();
        Task<CategoryDto> GetbyIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Category, bool>> filter);
        Task<List<CategoryDto>> GetbyFilterAsync(Expression<Func<Category, bool>> filter);
        Task<List<ProductDto>> GetProductsbyCategoryIdAsync(int categoryId);
    }
}
