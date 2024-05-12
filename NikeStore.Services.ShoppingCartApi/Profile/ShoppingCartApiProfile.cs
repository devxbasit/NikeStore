using NikeStore.Services.ShoppingCartApi.Models;
using NikeStore.Services.ShoppingCartApi.Models.Dto;

namespace NikeStore.Services.ShoppingCartApi.Profile;

public class ShoppingCartApiProfile : AutoMapper.Profile
{
    public ShoppingCartApiProfile()
    {
        CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
    }
}