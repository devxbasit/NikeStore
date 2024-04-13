using NikeStore.Services.CouponApi.Models;
using NikeStore.Services.CouponApi.Models.Dto;

namespace NikeStore.Services.CouponApi.Profile;

public class CouponApiProfile : AutoMapper.Profile
{
    public CouponApiProfile()
    {
        CreateMap<Coupon, CouponDto>();
        CreateMap<Coupon, CouponDto>().ReverseMap();
        
    }
}