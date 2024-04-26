using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

namespace NikeStore.Web.Service;
public class TokenProviderService : ITokenProviderService
{
    private readonly IHttpContextAccessor _contextAccessor;
    public TokenProviderService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
    }
}