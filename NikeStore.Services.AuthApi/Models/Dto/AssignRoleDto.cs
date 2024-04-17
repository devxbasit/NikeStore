using System.ComponentModel.DataAnnotations;

namespace NikeStore.Services.AuthApi.Models.Dto;

public class AssignRoleDto
{
    [Required] public string Email { get; set; }
    [Required] public string Role { get; set; }
}