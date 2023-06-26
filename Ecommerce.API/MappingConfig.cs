using AutoMapper;
using Ecommerce.Models.Models;
using Ecommerce.Models.Models.DTO.Product;
using Ecommerce.Models.Models.DTO.Response;

namespace Ecommerce.API
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();
            
        }
    }
}
