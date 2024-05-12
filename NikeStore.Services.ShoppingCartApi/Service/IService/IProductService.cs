using NikeStore.Services.ShoppingCartApi.Models.Dto;

namespace NikeStore.Services.ShoppingCartApi.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
