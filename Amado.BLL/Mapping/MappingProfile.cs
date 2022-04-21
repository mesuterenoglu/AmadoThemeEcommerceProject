
using Amado.Core.DTOs;
using Amado.Core.Entities;
using AutoMapper;

namespace Amado.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();

            CreateMap<AppRole, AppRoleDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Brand, BrandDto>().ReverseMap();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
        }
    }
}
