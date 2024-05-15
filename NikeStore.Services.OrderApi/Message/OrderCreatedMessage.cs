namespace NikeStore.Services.EmailApi.Message;

public class OrderCreatedMessage
{
    public int OrderHeaderId  { get; set; }
    public DateTime OrderCreatedDateTime { get; set; }
}
