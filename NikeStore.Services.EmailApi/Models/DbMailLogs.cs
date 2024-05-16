namespace NikeStore.Services.EmailApi.Models;

public class DbMailLogs
{
    public int Id { get; set; }
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public bool IsProcessed { get; set; }
}
