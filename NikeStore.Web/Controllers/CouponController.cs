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
        List<CouponDto>? couponList = new();

        ResponseDto? response = await _couponService.GetAllCouponsAsync();

        if (response is not null && response.IsSuccess)
        {
            couponList = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(couponList);
    }


    public async Task<IActionResult> CouponCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CouponCreate(CouponDto couponDto)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _couponService.CreateCouponAsync(couponDto);

            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Coupon Created Successfully!";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }

        return View(couponDto);
    }

    public async Task<IActionResult> CouponDelete(CouponDto couponDto)
    {
        ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Coupon deleted successfully";
            return RedirectToAction(nameof(CouponIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(couponDto);
    }
}