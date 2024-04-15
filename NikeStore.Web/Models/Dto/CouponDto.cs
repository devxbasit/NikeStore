using System.ComponentModel.DataAnnotations;

namespace NikeStore.Web.Models.Dto;

public class CouponDto
{
    public int CouponId { get; set; }
    [Required] public string CouponCode { get; set; } = null!;
    [Required] public int DiscountAmount { get; set; }
    [Required] public int MinAmount { get; set; }
}