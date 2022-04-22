using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.Core.Entities;
using Amado.Core.FilterModel;
using Amado.DataAccess.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductImageService _productImageService;

        public ProductService(IProductRepository productRepository,IMapper mapper,IProductImageService productImageService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productImageService = productImageService;
        }

        public async Task<string> AddAsync(ProductDto entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);
                await _productRepository.AddAsync(product);
                return Messages.Added;
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
                return await _productRepository.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> filter)
        {
            try
            {
                return await _productRepository.AnyAsync(filter);
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
                await _productRepository.DeleteAsync(id);
                return Messages.Deleted;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductDto>> GetAllActiveAsync()
        {
            try
            {
                var products = await _productRepository.GetAllActiveAsync();
                var dtos = _mapper.Map<List<ProductDto>>(products);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var dtos = _mapper.Map<List<ProductDto>>(products);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductDto>> GetAllInActiveAsync()
        {
            try
            {
                var products = await _productRepository.GetAllInActiveAsync();
                var dtos = _mapper.Map<List<ProductDto>>(products);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductDto>> GetbyFilterAsync(Expression<Func<Product, bool>> filter)
        {
            try
            {
                var products = await _productRepository.GetbyFilterAsync(filter);
                var dtos = _mapper.Map<List<ProductDto>>(products);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<ProductDto> GetbyIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetbyIdAsync(id);
                var dto = _mapper.Map<ProductDto>(product);
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductDto>> GetProductsbyCategoryIdAsync(int categoryId)
        {
            try
            {
                var products = await _productRepository.GetProductsbyCategoryIdAsync(categoryId);
                var dtos = _mapper.Map<List<ProductDto>>(products);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public List<ProductDto> GetProductsbyFilterAsync(FilterViewModel model)
        {
            try
            {
                var products =  _productRepository.GetProductsbyFilterAsync(model);
                var dtos = _mapper.Map<List<ProductDto>>(products);
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
                var images = await _productImageService.GetImagesByProductIdAsync(id);
                if (images != null)
                {
                    foreach (var image in images)
                    {
                        await _productImageService.RemoveFromDbAsync(image.Id);
                    }
                }
                await _productRepository.RemoveFromDbAsync(id);
                return Messages.RemovedFromDB;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(ProductDto entity)
        {
            try
            {
                var product = await _productRepository.GetbyIdAsync(entity.Id);
                product.Price = entity.Price;
                product.ProductName = entity.ProductName;
                product.StockShortage = entity.StockShortage;
                product.Description = entity.Description;
                product.UnitInfo = entity.UnitInfo;
                product.CategoryId = entity.CategoryId;
                product.BrandId = entity.BrandId;
                product.IsActive = entity.IsActive;
                await _productRepository.UpdateAsync(product);
                return Messages.Updated;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
