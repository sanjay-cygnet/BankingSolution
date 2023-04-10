namespace BuildingBlocks.EventBus.EventPublisherModel;

public class EmailPublisherModel
{
    public short TemplateId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string TemplateValues { get; set; } = string.Empty;
    public string Ccs { get; set; } = string.Empty;
    public string Tos { get; set; } = string.Empty;
    public string Bccs { get; set; } = string.Empty;
}
