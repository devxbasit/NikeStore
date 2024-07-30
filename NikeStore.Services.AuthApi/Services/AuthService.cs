using Microsoft.AspNetCore.Identity;
using NikeStore.Services.AuthApi.Data;
using NikeStore.Services.AuthApi.Models;
using NikeStore.Services.AuthApi.Models.Dto;
using NikeStore.Services.AuthApi.Services.IService;
using NikeStore.Services.AuthApi.Utility;

namespace NikeStore.Services.AuthApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;

    public AuthService(AppDbContext db, IJwtTokenGeneratorService jwtTokenGeneratorService,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        if (user is not null)
        {
            if (! await _roleManager.RoleExistsAsync(roleName))
            {
                //create role if it does not exist
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }

        return false;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || isValid == false)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }

        //if user was found , Generate JWT Token
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGeneratorService.GenerateToken(user, roles);

        UserDto userDTO = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDTO,
            Token = token
        };

        return loginResponseDto;
    }

    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            var roleResult = await AssignRole(user.Email, SD.Roles.Customer);

            if (result.Succeeded && roleResult)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    ID = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };

                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception ex)
        {
        }

        return "Error Encountered";
    }
}
