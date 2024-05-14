using NikeStore.Services.CouponApi.Models;
using NikeStore.Services.CouponApi.Models.Dto;

namespace NikeStore.Services.CouponApi.Profile;

public class OrderApiProfile : AutoMapper.Profile
{
    public OrderApiProfile()
    {
        CreateMap<Coupon, CouponDto>();
        CreateMap<Coupon, CouponDto>().ReverseMap();
        
    }
}