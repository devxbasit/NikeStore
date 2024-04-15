using NikeStore.Web.Models.Dto;

namespace NikeStore.Web.Service.IService;

public interface ICouponService
{
    Task<ResponseDto?> GetAllCouponsAsync();
    Task<ResponseDto?> GetCouponByIdAsync(int couponId);
    Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);
    Task<ResponseDto?> DeleteCouponAsync(int couponId);
    Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
    Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
}