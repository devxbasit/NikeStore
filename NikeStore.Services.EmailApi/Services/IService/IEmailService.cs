using NikeStore.Services.EmailApi.Message;
using NikeStore.Services.EmailApi.Models.Dto;

namespace NikeStore.Services.EmailApi.Services.IService;

public interface IEmailService
{
    Task EmailCartAndLog(CartDto cartDto);
    Task RegisterUserEmailAndLog(string email);
    Task LogOrderPlaced(RewardsMessage rewardsMessage);
}