using Microsoft.IdentityModel.Tokens;

namespace NikeStore.Services.AuthApi.Models;

public class JwtOptions
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}