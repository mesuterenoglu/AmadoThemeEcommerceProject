using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.Core.Entities;
using Amado.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services
{
    public class ProductImageService :IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageService(IProductImageRepository productImageRepository,IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(ProductImageDto entity)
        {
            try
            {
                var productImage = _mapper.Map<ProductImage>(entity);
                await _productImageRepository.AddAsync(productImage);
                return Messages.Added;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> AddImageServer(IFormFile formFile)
        {
            try
            {
                string fileName = "/img/product-img/" + Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{fileName}");
                await formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                return fileName;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(int id)
        {
            try
            {
                return await _productImageRepository.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<ProductImage, bool>> filter)
        {
            try
            {
                return await _productImageRepository.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                await _productImageRepository.DeleteAsync(id);
                return Messages.Deleted;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public string DeleteImageFromServer(string imagePath)
        {
            var filePath  = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{imagePath}");

            FileInfo info = new FileInfo(filePath);

            if (info != null)
            {
                System.IO.File.Delete(filePath);
            }
            return Messages.Deleted;
        }

        public async Task<List<ProductImageDto>> GetAllActiveAsync()
        {
            try
            {
                var productImages = await _productImageRepository.GetAllActiveAsync();
                var dtos = _mapper.Map<List<ProductImageDto>>(productImages);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductImageDto>> GetAllAsync()
        {
            try
            {
                var productImages = await _productImageRepository.GetAllAsync();
                var dtos = _mapper.Map<List<ProductImageDto>>(productImages);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductImageDto>> GetAllInActiveAsync()
        {
            try
            {
                var productImages = await _productImageRepository.GetAllInActiveAsync();
                var dtos = _mapper.Map<List<ProductImageDto>>(productImages);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductImageDto>> GetbyFilterAsync(Expression<Func<ProductImage, bool>> filter)
        {
            try
            {
                var productImages = await _productImageRepository.GetbyFilterAsync(filter);
                var dtos = _mapper.Map<List<ProductImageDto>>(productImages);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<ProductImageDto> GetbyIdAsync(int id)
        {
            try
            {
                var productImage = await _productImageRepository.GetbyIdAsync(id);
                var dto = _mapper.Map<ProductImageDto>(productImage);
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductImageDto>> GetImagesByProductIdAsync(int id)
        {
            try
            {
                var productImages = await _productImageRepository.GetImagesByProductIdAsync(id);
                var dtos = _mapper.Map<List<ProductImageDto>>(productImages);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> RemoveFromDbAsync(int id)
        {
            try
            {
                var image = await _productImageRepository.GetbyIdAsync(id);
                DeleteImageFromServer(image.Url);
                await _productImageRepository.RemoveFromDbAsync(id);
                return Messages.RemovedFromDB;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(ProductImageDto entity)
        {
            try
            {
                var productImage = _mapper.Map<ProductImage>(entity);
                await _productImageRepository.UpdateAsync(productImage);
                return Messages.Updated;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
