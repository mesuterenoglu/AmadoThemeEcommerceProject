

using Amado.Core.DTOs;
using Amado.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services.Interfaces
{
    public interface IBrandService
    {
        Task<string> AddAsync(BrandDto entity);
        Task<string> DeleteAsync(int id);
        Task<string> RemoveFromDbAsync(int id);
        Task<string> UpdateAsync(BrandDto entity);
        Task<List<BrandDto>> GetAllAsync();
        Task<List<BrandDto>> GetAllInActiveAsync();
        Task<List<BrandDto>> GetAllActiveAsync();
        Task<BrandDto> GetbyIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Brand, bool>> filter);
        Task<List<BrandDto>> GetbyFilterAsync(Expression<Func<Brand, bool>> filter);
        Task<List<ProductDto>> GetProductsbyBrandIdAsync(int brandId);
    }
}
