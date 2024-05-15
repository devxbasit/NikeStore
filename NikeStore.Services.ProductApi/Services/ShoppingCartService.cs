using Newtonsoft.Json;
using NikeStore.Services.ProductApi.Models.Dto;
using NikeStore.Services.ProductApi.Services.IService;

namespace NikeStore.Services.ProductApi.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ShoppingCartService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }

    public async Task<bool> RemoveProductFromAllCart(int productId)
    {
        var client = _httpClientFactory.CreateClient("ShoppingCartClient");
        var response = await client.PostAsync($"/api/cart/RemoveProductFromAllCart/{productId}", null);
        var apiContent = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        return resp.IsSuccess;
    }
}
