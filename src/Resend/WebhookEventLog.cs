using System.Text.Json.Serialization;

namespace Resend;

public class WebhookEventLog
{
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    [JsonPropertyName( "type" )]
    public string Type { get; set; } = default!;

    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    [JsonPropertyName( "status" )]
    public WebhookEventLogStatus Status { get; set; }
}
