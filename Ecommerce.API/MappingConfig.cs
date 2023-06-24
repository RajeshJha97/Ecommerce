using AutoMapper;
using Ecommerce.Models.Models;
using Ecommerce.Models.Models.DTO;

namespace Ecommerce.API
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
        }
    }
}
