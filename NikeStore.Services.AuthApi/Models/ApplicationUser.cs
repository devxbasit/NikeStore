using Microsoft.AspNetCore.Identity;

namespace NikeStore.Services.AuthApi.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}