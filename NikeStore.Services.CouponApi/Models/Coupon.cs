using System.ComponentModel.DataAnnotations;

namespace NikeStore.Services.CouponApi.Models;

public class Coupon
{
    [Key] public int CouponId { get; set; }
    [Required] public string CouponCode { get; set; } = null!;
    [Required] public int DiscountAmount { get; set; }
    [Required] public int MinAmount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastUpdatedDateTime { get; set; }
}