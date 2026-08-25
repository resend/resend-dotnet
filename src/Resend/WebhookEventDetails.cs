using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

public class WebhookEventDetails
{
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    [JsonPropertyName( "type" )]
    public string Type { get; set; } = default!;

    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    [JsonPropertyName( "status" )]
    public WebhookEventLogStatus Status { get; set; }

    [JsonPropertyName( "next_attempt_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentNextAttempt { get; set; }

    [JsonPropertyName( "payload" )]
    public JsonElement Payload { get; set; }
}
