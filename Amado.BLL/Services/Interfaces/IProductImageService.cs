

using Amado.Core.DTOs;
using Amado.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services.Interfaces
{
    public interface IProductImageService
    {
        Task<string> AddAsync(ProductImageDto entity);
        Task<string> DeleteAsync(int id);
        Task<string> RemoveFromDbAsync(int id);
        Task<string> UpdateAsync(ProductImageDto entity);
        Task<List<ProductImageDto>> GetAllAsync();
        Task<List<ProductImageDto>> GetAllInActiveAsync();
        Task<List<ProductImageDto>> GetAllActiveAsync();
        Task<ProductImageDto> GetbyIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<ProductImage, bool>> filter);
        Task<List<ProductImageDto>> GetbyFilterAsync(Expression<Func<ProductImage, bool>> filter);
        Task<string> AddImageServer(IFormFile formFile);
        string DeleteImageFromServer(string imagePath);
        Task<List<ProductImageDto>> GetImagesByProductIdAsync(int id);

    }
}
