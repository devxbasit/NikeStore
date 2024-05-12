using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NikeStore.Services.ShoppingCartApi.Models.Dto;

namespace NikeStore.Services.ShoppingCartApi.Models;

public class CartDetails
{
    [Key]
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    
    
    
    
    [ForeignKey("CartHeaderId")]
    public CartHeader CartHeader { get; set; }
    
    [NotMapped]
    public ProductDto Product { get; set; }
}