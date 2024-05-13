﻿using NikeStore.Web.Utility;

namespace NikeStore.Web.Models.Dto
{
    public class RequestDto
    {
        public SD.ApiType ApiType { get; set; } = SD.ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        public SD.ContentType ContentType { get; set; } = SD.ContentType.Json;
    }
}