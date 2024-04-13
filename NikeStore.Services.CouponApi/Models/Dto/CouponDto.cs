namespace NikeStore.Services.CouponApi.Models.Dto;

public class CouponDto
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = null!;
    public int DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}