using NikeStore.Services.EmailApi.Message;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Models.Dto;

namespace NikeStore.Services.EmailApi.Services.IService;

public interface IDbLogService
{
    Task LogToDb(DbMailLogs log);
}
