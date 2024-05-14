using NikeStore.Services.OrderApi.Models;
using NikeStore.Services.OrderApi.Models.Dto;

namespace NikeStore.Services.OrderApi.Profile;

public class OrderApiProfile : AutoMapper.Profile
{
    public OrderApiProfile()
    {
        CreateMap<OrderHeaderDto, CartHeaderDto>().ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

        CreateMap<CartDetailsDto, OrderDetailsDto>()
            .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

        CreateMap<OrderDetailsDto, CartDetailsDto>();
        CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
        CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
    }
}