namespace NikeStore.Services.ProductApi.Services.IService;

public interface IShoppingCartService
{
    Task<bool> RemoveProductFromAllCart(int productId);
}
