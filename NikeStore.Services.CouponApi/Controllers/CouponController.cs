using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NikeStore.Services.CouponApi.Data;
using NikeStore.Services.CouponApi.Models;
using NikeStore.Services.CouponApi.Models.Dto;

namespace NikeStore.Services.CouponApi.Controllers;

[Route("api/coupon")]
[ApiController]
[Authorize]
public class CouponAPIController : ControllerBase
{
    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;

    public CouponAPIController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Post()
    {
        using StreamReader reader = new StreamReader(Request.Body, leaveOpen: false);
        var stringContent = await reader.ReadToEndAsync();
        CouponDto couponDto = JsonConvert.DeserializeObject<CouponDto>(stringContent);

        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);

            if (_db.Coupons.FirstOrDefault(x => x.CouponCode.ToLower() == coupon.CouponCode.ToLower()) is null)
            {
                _db.Coupons.Add(coupon);
                _db.SaveChanges();

                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "inr",
                    Id = couponDto.CouponCode,
                };
                var service = new Stripe.CouponService();
                //service.Create(options);

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "Coupon code already exists!";
            }
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Update()
    {
        using StreamReader reader = new StreamReader(Request.Body, leaveOpen: false);
        var stringContent = await reader.ReadToEndAsync();
        CouponDto couponDto = JsonConvert.DeserializeObject<CouponDto>(stringContent);

        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);

            if (_db.Coupons.AsNoTracking().FirstOrDefault(x => x.CouponId == coupon.CouponId) is not null)
            {
                _db.Coupons.Update(coupon);
                _db.SaveChanges();

                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "inr",
                    Id = couponDto.CouponCode,
                };
                var service = new Stripe.CouponService();
                //service.Create(options);

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid Coupon Id!";
            }
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Coupon> objList = _db.Coupons.ToList();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u => u.CouponId == id);
            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    // [HttpPut]
    // [Authorize(Roles = "ADMIN")]
    // public ResponseDto Put([FromBody] CouponDto couponDto)
    // {
    //     try
    //     {
    //         Coupon obj = _mapper.Map<Coupon>(couponDto);
    //         _db.Coupons.Update(obj);
    //         _db.SaveChanges();
    //
    //         _response.Result = _mapper.Map<CouponDto>(obj);
    //     }
    //     catch (Exception ex)
    //     {
    //         _response.IsSuccess = false;
    //         _response.Message = ex.Message;
    //     }
    //
    //     return _response;
    // }
    //


    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public ResponseDto Delete(int id)
    {
        try
        {
            Coupon obj = _db.Coupons.First(u => u.CouponId == id);
            _db.Coupons.Remove(obj);
            _db.SaveChanges();

            var service = new Stripe.CouponService();
            // service.Delete(obj.CouponCode);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
