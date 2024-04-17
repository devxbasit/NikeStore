using NikeStore.Services.AuthApi.Models.Dto;

namespace NikeStore.Services.AuthApi.Services.IService;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
    
}