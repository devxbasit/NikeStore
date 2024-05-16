using NikeStore.Services.EmailApi.Models.Dto;

namespace NikeStore.Services.EmailApi.Message;

public class EmailCartMessage
{
    public CartHeader CartHeader { get; set; }
    public IEnumerable<CartDetailsDto> CartDetails { get; set; }
}
