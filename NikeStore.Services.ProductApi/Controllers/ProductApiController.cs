using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NikeStore.Services.ProductApi.Data;
using NikeStore.Services.ProductApi.Models;
using NikeStore.Services.ProductApi.Models.Dto;
using NikeStore.Services.ProductApi.Services.IService;

namespace NikeStore.Services.ProductApi.Controllers;

[Route("api/product")]
[ApiController]
public class ProductApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;
    private readonly IShoppingCartService _shoppingCartService;

    public ProductApiController(AppDbContext db, IShoppingCartService shoppingCartService,  IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Product> productList = _db.Products.ToList();
            _response.Result = _mapper.Map<IEnumerable<ProductDto>>(productList);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{productId:int}")]
    public ResponseDto Get(int productId)
    {
        try
        {
            Product obj = _db.Products.First(u => u.ProductId == productId);
            _response.Result = _mapper.Map<ProductDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public ResponseDto Post(ProductDto ProductDto)
    {
        try
        {
            Product product = _mapper.Map<Product>(ProductDto);
            _db.Products.Add(product);
            _db.SaveChanges();

            if (ProductDto.Image != null)
            {
                string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                string filePath = @"wwwroot/ProductImages/" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);
                if (file.Exists)
                {
                    file.Delete();
                }

                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    ProductDto.Image.CopyTo(fileStream);
                }

                var baseUrl =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                product.ImageLocalPath = filePath;
            }
            else
            {
                product.ImageUrl = "https://placehold.co/600x400";
            }

            _db.Products.Update(product);
            _db.SaveChanges();
            _response.Result = _mapper.Map<ProductDto>(product);
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
    public ResponseDto Put(ProductDto ProductDto)
    {
        try
        {
            Product product = _mapper.Map<Product>(ProductDto);

            if (ProductDto.Image != null)
            {
                if (!string.IsNullOrEmpty(product.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

                string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                string filePath = @"wwwroot/ProductImages/" + fileName;
                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    ProductDto.Image.CopyTo(fileStream);
                }

                var baseUrl =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                product.ImageLocalPath = filePath;
            }


            _db.Products.Update(product);
            _db.SaveChanges();

            _response.Result = _mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpDelete("{productId:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Delete(int productId)
    {
        try
        {
            Product obj = _db.Products.First(u => u.ProductId == productId);
            if (!string.IsNullOrEmpty(obj.ImageLocalPath))
            {
                var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), obj.ImageLocalPath);
                FileInfo file = new FileInfo(oldFilePathDirectory);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

            _db.Products.Remove(obj);
            await _shoppingCartService.RemoveProductFromAllCart(productId);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
