using NikeStore.Services.EmailApi.Models;

namespace NikeStore.Services.EmailApi.Services.IService;

public interface ISmtpMailService
{
    public Task<bool> SendMail(MailMessage mailMessage);
}
