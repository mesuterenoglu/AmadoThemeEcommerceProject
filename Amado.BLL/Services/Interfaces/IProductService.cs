

using Amado.Core.DTOs;
using Amado.Core.Entities;
using Amado.Core.FilterModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<string> AddAsync(ProductDto entity);
        Task<string> DeleteAsync(int id);
        Task<string> RemoveFromDbAsync(int id);
        Task<string> UpdateAsync(ProductDto entity);
        Task<List<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetAllInActiveAsync();
        Task<List<ProductDto>> GetAllActiveAsync();
        Task<ProductDto> GetbyIdAsync(int id);
        Task<List<ProductDto>> GetProductsbyCategoryIdAsync(int categoryId);
        Task<bool> AnyAsync(Expression<Func<Product, bool>> filter);
        Task<List<ProductDto>> GetbyFilterAsync(Expression<Func<Product, bool>> filter);
        List<ProductDto> GetProductsbyFilterAsync(FilterViewModel model);
    }
}
