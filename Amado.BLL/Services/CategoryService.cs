using Amado.BLL.Services.Interfaces;
using Amado.Common.Utilities;
using Amado.Core.DTOs;
using Amado.Core.Entities;
using Amado.DataAccess.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amado.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(CategoryDto entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                await _categoryRepository.AddAsync(category);
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
                return await _categoryRepository.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> filter)
        {
            try
            {
                return await _categoryRepository.AnyAsync(filter);
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
                await _categoryRepository.DeleteAsync(id);
                return Messages.Deleted;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CategoryDto>> GetAllActiveAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllActiveAsync();
                var dtos = _mapper.Map<List<CategoryDto>>(categories);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var dtos = _mapper.Map<List<CategoryDto>>(categories);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CategoryDto>> GetAllInActiveAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllInActiveAsync();
                var dtos = _mapper.Map<List<CategoryDto>>(categories);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CategoryDto>> GetbyFilterAsync(Expression<Func<Category, bool>> filter)
        {
            try
            {
                var categories = await _categoryRepository.GetbyFilterAsync(filter);
                var dtos = _mapper.Map<List<CategoryDto>>(categories);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<CategoryDto> GetbyIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetbyIdAsync(id);
                var dto = _mapper.Map<CategoryDto>(category);
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
                var products = await _categoryRepository.GetProductsbyCategoryIdAsync(categoryId);
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
                await _categoryRepository.RemoveFromDbAsync(id);
                return Messages.RemovedFromDB;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(CategoryDto entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                await _categoryRepository.UpdateAsync(category);
                return Messages.Updated;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
