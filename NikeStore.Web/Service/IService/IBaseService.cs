using NikeStore.Web.Models.Dto;

namespace NikeStore.Web.Service.IService;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}