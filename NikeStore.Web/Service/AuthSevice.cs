using NikeStore.Web.Models.Dto;
using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

namespace NikeStore.Web.Service;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;
    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.AuthApiBase + "/api/auth/AssignRole"
        });
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = loginRequestDto,
            Url = SD.AuthApiBase + "/api/auth/login"
        }, withBearer: false);
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.AuthApiBase + "/api/auth/register"
        }, withBearer: false);
    }
}