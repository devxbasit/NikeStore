namespace NikeStore.Web.Utility;

// static details
public class SD
{
    public static string CouponApiBase { get; set; }
    public static string ProducApiBase { get; set; }
    public static string AuthApiBase { get; set; }
    public static string ShoppingCartApiBase { get; set; }
    public static string OrderApiBase { get; set; }
    
    

    // constants here
    public static class Roles
    {
        public const string Admin = "ADMIN";
        public const string Customer = "CUSTOMER";
    }
    
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

    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string ReadyForPickup = "ReadyForPickup";
        public const string Completed = "Completed";
        public const string Refunded = "Refunded";
        public const string Cancelled = "Cancelled";
    }
}