namespace NikeStore.Services.OrderApi.Utility
{
    public class SD
    {
        public static class OrderStatus
        {
            public const string Pending = "Pending";
            public const string Approved = "Approved";
            public const string ReadyForPickup = "ReadyForPickup";
            public const string Completed = "Completed";
            public const string Refunded = "Refunded";
            public const string Cancelled = "Cancelled";
        }
        
        public static class Roles
        {
            public const string Admin = "ADMIN";
            public const string Customer = "CUSTOMER";
        }
    }
}