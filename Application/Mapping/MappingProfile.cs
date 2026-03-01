using Application.DTOs.ResponseDTOs.User;
using Application.DTOs.ResponseDTOs.Category;
using Application.DTOs.ResponseDTOs.Product;
using Application.DTOs.ResponseDTOs.ProductImage;
using Application.DTOs.ResponseDTOs.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Service, ServiceResponse>();
            CreateMap<ProductImage, ProductImageResponse>();
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null));
        }
    }
}
