
namespace NikeStore.Services.ShoppingCartApi.Models.Dto
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        
        
        
        public CartHeaderDto? CartHeader { get; set; }
        public ProductDto? Product { get; set; }
    }
}
