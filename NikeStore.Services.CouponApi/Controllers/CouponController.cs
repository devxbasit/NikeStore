using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NikeStore.Services.CouponApi.Data;
using NikeStore.Services.CouponApi.Models;
using NikeStore.Services.CouponApi.Models.Dto;

namespace NikeStore.Services.CouponApi.Controllers;

[ApiController]
[Route("api/coupon")]
[Authorize]
public class CouponController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly ResponseDto _response;

    public CouponController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpGet]
    public ActionResult GetCoupons()
    {
        try
        {
            var couponList = _db.Coupons.ToList();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return Ok(_response);
    }


    [HttpGet("{couponId:int}", Name = nameof(GetCouponById))]
    public IActionResult GetCouponById(int couponId)
    {
        try
        {
            // or we can use FirstOrDefault, .First() will throw error if not found.
            var coupon = _db.Coupons.First(c => c.CouponId == couponId);
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return Ok(_response);
    }


    [HttpGet("GetByCode/{couponCode}")]
    public IActionResult GetCouponByCode(string couponCode)
    {
        try
        {
            var coupon = _db.Coupons.First(c => c.CouponCode.ToLower() == couponCode.ToLower());
            _response.Result = _mapper.Map<CouponDto>(coupon);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }

        return Ok(_response);
    }


    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public IActionResult CreateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(coupon);
            _db.SaveChanges();

            _response.Result = _mapper.Map<CouponDto>(coupon);
            return CreatedAtRoute(nameof(GetCouponById), new { couponId = coupon.CouponId }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
            return BadRequest(_response);
        }
    }


    [HttpDelete("{couponId:int}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult DeleteCoupon(int couponId)
    {
        try
        {
            Coupon coupon = _db.Coupons.First(c => c.CouponId == couponId);
            _db.Coupons.Remove(coupon);
            _db.SaveChanges();
            _response.Result = coupon;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
            return NotFound(_response);
        }
    }
    
    
    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public IActionResult UpdateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(coupon);
            _db.SaveChanges();

            _response.Result = _mapper.Map<CouponDto>(coupon);
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
            return NotFound(_response);
        }
    }
}