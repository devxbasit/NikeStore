namespace NikeStore.Services.EmailApi.Message;

public class OrderCreatedMessage
{
    public int OrderHeaderId  { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double OrderTotal { get; set; }
    public DateTime OrderCreatedDateTime { get; set; }
}
