using NikeStore.Services.ProductApi.Models;
using NikeStore.Services.ProductApi.Models.Dto;

namespace NikeStore.Services.ProductApi.Profile;

public class ProductApiProfile : AutoMapper.Profile
{
    public ProductApiProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}