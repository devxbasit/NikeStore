using NikeStore.Services.AuthApi.Models;

namespace NikeStore.Services.AuthApi.Services.IService;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}