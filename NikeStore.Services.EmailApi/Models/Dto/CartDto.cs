namespace NikeStore.Services.EmailApi.Models.Dto;

public class CartDto
{
    public CartHeader Car { get; set; }
    public IEnumerable<CartDetailsDto> Cart { get; set; }
}