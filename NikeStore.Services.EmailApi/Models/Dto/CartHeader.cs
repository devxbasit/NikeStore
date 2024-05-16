using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NikeStore.Services.EmailApi.Models.Dto;

public class CartHeader
{
    public int CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double CartTotal { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
}
