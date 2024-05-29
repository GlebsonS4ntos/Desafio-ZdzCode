using AutoMapper;
using Desafio.Api.Dtos.Category;
using Desafio.Api.Dtos.Product;
using Desafio.Api.Entities;

namespace Desafio.Api.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
