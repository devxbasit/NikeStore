using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using NikeStore.Web.Models.Dto;
using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

namespace NikeStore.Web.Service;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("NikeStoreAPI");

            HttpRequestMessage httpMessage = new();
            httpMessage.Headers.Add(HttpRequestHeader.Accept.ToString(), MediaTypeNames.Application.Json);

            // token

            httpMessage.RequestUri = new Uri(requestDto.Url);

            if (requestDto.Data is not null)
            {
                httpMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8,
                    MediaTypeNames.Application.Json);
            }

            httpMessage.Method = requestDto.ApiType switch
            {
                SD.ApiType.POST => HttpMethod.Post,
                SD.ApiType.PUT => HttpMethod.Put,
                SD.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };

            HttpResponseMessage? apiResponse = null;
            apiResponse = await httpClient.SendAsync(httpMessage);


            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found!" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied!" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorised!" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error!" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (Exception e)
        {
            return new() { IsSuccess = false, Message = e.Message };
        }
    }
}