using NikeStore.Web.Models.Dto;
using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

namespace NikeStore.Web.Service;

public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    #region GET
    
    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
      
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponApiBase}/api/coupon",
            });

    }

    public async  Task<ResponseDto?> GetCouponByIdAsync(int couponId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.CouponApiBase}/api/coupon/{couponId}",
        });
    }

    public async  Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.CouponApiBase}/api/coupon/{couponCode}",
        });
    }

    #endregion

    #region POST, PUT, PATCH, DELETE

    public async  Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            Data = couponDto,
            ApiType = SD.ApiType.PUT,
            Url = $"{SD.CouponApiBase}/api/coupon",
        });
    }

    public async  Task<ResponseDto?> DeleteCouponAsync(int couponId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.DELETE,
            Url = $"{SD.CouponApiBase}/api/coupon/{couponId}",
        });
    }

    public async  Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            Data = couponDto,
            ApiType = SD.ApiType.POST,
            Url = $"{SD.CouponApiBase}/api/coupon",
        });
    }

    #endregion
    
}