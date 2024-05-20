using System.Net;
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
    private readonly ITokenProviderService _tokenProviderService;

    public BaseService(IHttpClientFactory httpClientFactory, ITokenProviderService tokenProviderService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProviderService = tokenProviderService;
    }

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
    {
        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("NikeStoreAPI");
            HttpRequestMessage httpMessage = new();

            if (requestDto.ContentType == SD.ContentType.MultipartFormData)
            {
                httpMessage.Headers.Add(HttpRequestHeader.Accept.ToString(), "*/*");
                //httpMessage.Headers.Add(HttpRequestHeader.ContentType.ToString(), MediaTypeNames.Multipart.FormData);
            }
            else
            {
                httpMessage.Headers.Add(HttpRequestHeader.Accept.ToString(), MediaTypeNames.Application.Json);
                //httpMessage.Headers.Add(HttpRequestHeader.ContentType.ToString(), MediaTypeNames.Application.Json);
            }

            // token
            if (withBearer)
            {
                var token = _tokenProviderService.GetToken();
                httpMessage.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"Bearer {token}");
            }

            httpMessage.RequestUri = new Uri(requestDto.Url);

            if (requestDto.ContentType == SD.ContentType.MultipartFormData)
            {
                var content = new MultipartFormDataContent();

                foreach (var prop in requestDto.Data.GetType().GetProperties())
                {
                    var value = prop.GetValue(requestDto.Data);
                    if (value is FormFile)
                    {
                        var file = (FormFile)value;
                        if (file != null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                    }
                }

                httpMessage.Content = content;
            }
            else
            {
                if (requestDto.Data is not null)
                {
                    httpMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, MediaTypeNames.Application.Json);
                }
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
                case HttpStatusCode.BadRequest:
                    return new() { IsSuccess = false, Message = "Bad Request!" };
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
