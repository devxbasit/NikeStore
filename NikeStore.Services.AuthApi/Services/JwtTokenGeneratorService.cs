using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NikeStore.Services.AuthApi.Models;
using NikeStore.Services.AuthApi.Services.IService;

namespace NikeStore.Services.AuthApi.Services;

public class JwtTokenGeneratorService : IJwtTokenGeneratorService
{
    private JwtOptions _jwtOptions;

    public JwtTokenGeneratorService(IOptionsMonitor<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.CurrentValue;
        jwtOptions.OnChange((newOptionsValue) => _jwtOptions = newOptionsValue);

    }

    public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
    {

        // used for creating and validating tokens
        var tokenHandler = new JwtSecurityTokenHandler();

        var claimsList = GetClaimsList(applicationUser, roles);
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,

            Expires = DateTime.UtcNow.AddDays(365),

            // here we add payload
            Subject = new ClaimsIdentity(claimsList),

            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> GetClaimsList(ApplicationUser applicationUser, IEnumerable<string> roles)
    {
        var claimList = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
            new Claim(JwtRegisteredClaimNames.Name, applicationUser.Name),
        };

        claimList.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        return claimList;
    }
}
