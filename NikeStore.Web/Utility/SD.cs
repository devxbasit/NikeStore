namespace NikeStore.Web.Utility;

// static details
public class SD
{
    public static string CouponApiBase { get; set; }

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum ContentType
    {
        Json,
        MultipartFormData
    }
}