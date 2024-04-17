using System.ComponentModel.DataAnnotations;

namespace NikeStore.Web.Models.Dto;

public class RegistrationRequestDto
{
    [Required] public string Email { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public string Password { get; set; }
    public string? Role { get; set; }
}