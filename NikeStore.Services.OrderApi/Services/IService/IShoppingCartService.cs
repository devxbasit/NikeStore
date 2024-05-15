namespace NikeStore.Services.OrderApi.Services.IService;

public interface IShoppingCartService
{
    Task<bool> ClearCart(string userId);
}
