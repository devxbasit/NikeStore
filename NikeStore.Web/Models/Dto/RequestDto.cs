// The "static" modifier imports the static members and nested types from a single type rather than importing all the types in a namespace

using NikeStore.Web.Utility;
using static NikeStore.Web.Utility.SD;

namespace NikeStore.Web.Models.Dto;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object? Data { get; set; }
    public string AccessToken { get; set; }

    public ContentType ContentType { get; set; } = SD.ContentType.Json;
}