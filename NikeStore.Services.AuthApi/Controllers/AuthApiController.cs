using Microsoft.AspNetCore.Mvc;
using NikeStore.Services.AuthApi.Messages;
using NikeStore.Services.AuthApi.Models.Dto;
using NikeStore.Services.AuthApi.RabbitMqProducer;
using NikeStore.Services.AuthApi.Services.IService;

namespace NikeStore.Services.AuthApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthAPIController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly IRabbitMqAuthMessageProducer _rabbitMqAuthMessageProducer;
    protected ResponseDto _response;

    public AuthAPIController(IAuthService authService, IConfiguration configuration,
        IRabbitMqAuthMessageProducer rabbitMqAuthMessageProducer)
    {
        _authService = authService;
        _configuration = configuration;
        _rabbitMqAuthMessageProducer = rabbitMqAuthMessageProducer;
        _response = new();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            _response.IsSuccess = false;
            _response.Message = errorMessage;
            return BadRequest(_response);
        }

        var queueName = _configuration.GetValue<string>("RabbitMQSetting:QueueNames:UserRegisteredQueue");
        var message = new UserRegisteredMessage()
        {
            Name = model.Name,
            Email = model.Email,
        };

        _rabbitMqAuthMessageProducer.SendMessage(message, queueName);
        return Ok(_response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return BadRequest(_response);
        }

        _response.Result = loginResponse;
        return Ok(_response);
    }


    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
    {
        var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
        if (!assignRoleSuccessful)
        {
            _response.IsSuccess = false;
            _response.Message = "Error encountered";
            return BadRequest(_response);
        }

        return Ok(_response);
    }
}
