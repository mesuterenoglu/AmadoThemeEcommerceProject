
using Amado.Core.DTOs;
using Amado.MVC.Areas.Admin.Models.Category;
using Amado.MVC.Areas.Admin.Models.Product;
using Amado.MVC.Models.User;
using AutoMapper;

namespace Amado.MVC.MappingClient
{
    public class MappingProfileClient : Profile
    {
        public MappingProfileClient()
        {
            CreateMap<RegisterViewModel, AppUserDto>();

            CreateMap<AddProductViewModel, ProductDto>()
                .ForMember(dest => dest.ProductImages, act => act.Ignore());

            CreateMap<UpdateProductViewModel, ProductDto>().ReverseMap();

        }
    }
}
