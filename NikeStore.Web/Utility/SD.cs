namespace NikeStore.Web.Utility;

// static details
public class SD
{
    public static string CouponApiBase { get; set; }
    public static string AuthApiBase { get; set; }

    // constants here
    public const string RoleAdmin = "ADMIN";
    public const string RoleCustomer = "Customer";
    public const string TokenCookie = "JWTToken";
    
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