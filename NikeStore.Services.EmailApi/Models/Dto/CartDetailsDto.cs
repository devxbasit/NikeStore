namespace NikeStore.Services.EmailApi.Models.Dto;

public class CartDetailsDto
{
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public CartDetailsDto? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}