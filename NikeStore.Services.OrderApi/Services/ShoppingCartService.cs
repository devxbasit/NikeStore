using Newtonsoft.Json;
using NikeStore.Services.OrderApi.Models.Dto;
using NikeStore.Services.OrderApi.Services.IService;

namespace NikeStore.Services.OrderApi.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ShoppingCartService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }

    public async Task<bool> ClearCart(string userId)
    {
        var client = _httpClientFactory.CreateClient("ShoppingCartClient");
        var response = await client.PostAsync($"/api/cart/ClearCart/{userId}", null);
        var apiContet = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
        return resp.IsSuccess;
    }
}
