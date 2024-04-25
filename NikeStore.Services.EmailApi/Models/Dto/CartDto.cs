namespace NikeStore.Services.EmailApi.Models.Dto;

public class CartDto
{
    public CartHeader CartHeader { get; set; }
    public IEnumerable<CartDetailsDto> CartDetails { get; set; }
}