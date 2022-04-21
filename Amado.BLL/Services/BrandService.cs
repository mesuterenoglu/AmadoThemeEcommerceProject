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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<string> AddAsync(BrandDto entity)
        {
            try
            {
                var brand = _mapper.Map<Brand>(entity);
                await _brandRepository.AddAsync(brand);
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
                return await _brandRepository.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<Brand, bool>> filter)
        {
            try
            {
                return await _brandRepository.AnyAsync(filter);
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
                await _brandRepository.DeleteAsync(id);
                return Messages.Deleted;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<BrandDto>> GetAllActiveAsync()
        {
            try
            {
                var brands = await _brandRepository.GetAllActiveAsync();
                var dtos = _mapper.Map<List<BrandDto>>(brands);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<BrandDto>> GetAllAsync()
        {
            try
            {
                var brands = await _brandRepository.GetAllAsync();
                var dtos = _mapper.Map<List<BrandDto>>(brands);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<BrandDto>> GetAllInActiveAsync()
        {
            try
            {
                var brands = await _brandRepository.GetAllInActiveAsync();
                var dtos = _mapper.Map<List<BrandDto>>(brands);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<BrandDto>> GetbyFilterAsync(Expression<Func<Brand, bool>> filter)
        {
            try
            {
                var brands = await _brandRepository.GetbyFilterAsync(filter);
                var dtos = _mapper.Map<List<BrandDto>>(brands);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<BrandDto> GetbyIdAsync(int id)
        {
            try
            {
                var brand = await _brandRepository.GetbyIdAsync(id);
                var dto = _mapper.Map<BrandDto>(brand);
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<ProductDto>> GetProductsbyBrandIdAsync(int brandId)
        {
            try
            {
                var products = await _brandRepository.GetProductsbyBrandIdAsync(brandId);
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
                await _brandRepository.RemoveFromDbAsync(id);
                return Messages.RemovedFromDB;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(BrandDto entity)
        {
            try
            {
                var brand = _mapper.Map<Brand>(entity);
                await _brandRepository.UpdateAsync(brand);
                return Messages.Updated;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
