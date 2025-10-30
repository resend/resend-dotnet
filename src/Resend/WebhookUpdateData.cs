using System.Text.Json.Serialization;

namespace Resend;

public class WebhookUpdateData
{
    [JsonPropertyName( "endpoint" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? EndpointUrl { get; set; }

    [JsonPropertyName( "events" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<WebhookEventType>? Events { get; set; }

    [JsonPropertyName( "status" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public WebhookStatus? Status { get; set; }
}
