using NikeStore.Services.ShoppingCartApi.Models.Dto;

namespace NikeStore.Services.ShoppingCartApi.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
