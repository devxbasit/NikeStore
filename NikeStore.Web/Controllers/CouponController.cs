using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NikeStore.Web.Models.Dto;
using NikeStore.Web.Service.IService;

namespace NikeStore.Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    public async Task<IActionResult> CouponIndex()
    {
        return View();
    }

    [HttpGet]
    public async Task<ResponseDto> GetAllCoupons()
    {
        List<CouponDto>? list = new();

        ResponseDto? response = await _couponService.GetAllCouponsAsync();

        if (response != null && response.IsSuccess)
        {
            response.Result = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
        }

        return response;
    }

    public async Task<IActionResult> CouponCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CouponCreate(CouponDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _couponService.CreateCouponAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon created successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }

        return View(model);
    }


    [HttpPost]
    public async Task<ResponseDto> CouponDelete([FromBody]CouponDto couponDto)
    {
        ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

        if (response != null && response.IsSuccess)
        {
            response.IsSuccess = true;
        }
        else
        {
            response.IsSuccess = false;
        }

        return response;
    }
}
