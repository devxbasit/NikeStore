namespace NikeStore.Services.EmailApi.Models;

public class MailKitConnectionOptions
{
    public string SenderMailAddressHost { get; set; }
    public int SenderMailAddressHostPort { get; set; }
    public string SenderMailAddress { get; set; }
    public string SenderMailAddressPassword { get; set; }
}
