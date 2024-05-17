using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NikeStore.Web.Models.Dto;
using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

namespace NikeStore.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProviderService _tokenProvider;

    public AuthController(IAuthService authService, ITokenProviderService tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();
        return View(loginRequestDto);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto obj)
    {
        ResponseDto responseDto = await _authService.LoginAsync(obj);

        if (responseDto != null && responseDto.IsSuccess)
        {
            LoginResponseDto loginResponseDto =
                JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

            var role = await SignInUser(loginResponseDto);
            _tokenProvider.SetToken(loginResponseDto.Token);

            return role.ToLower() == "admin" ? RedirectToAction("OrderIndex", "Order") : RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["error"] = responseDto.Message;
            return View(obj);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto obj)
    {
        var IsAdminRegistration = false;

        if (obj.Name.Split("#").Length > 0 && obj.Name.Split("#").First().ToLower() == "root")
        {
            IsAdminRegistration = true;
            obj.Name = obj.Name.Split("#")[1];
        }


        ResponseDto result = await _authService.RegisterAsync(obj);
        ResponseDto assingRole;

        if (result != null && result.IsSuccess)
        {
            obj.Role = IsAdminRegistration ? SD.Roles.Admin : SD.Roles.Customer;

            assingRole = await _authService.AssignRoleAsync(obj);
            if (assingRole != null && assingRole.IsSuccess)
            {
                TempData["success"] = "Registration Successful";
                return RedirectToAction(nameof(Login));
            }
        }
        else
        {
            TempData["error"] = result.Message;
        }

        return View(obj);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Index", "Home");
    }


    private async Task<string> SignInUser(LoginResponseDto model)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(model.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

        identity.AddClaim(new Claim(ClaimTypes.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return jwt.Claims.FirstOrDefault(u => u.Type == "role").Value;
    }
}
