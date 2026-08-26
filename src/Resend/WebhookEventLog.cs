using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A webhook event delivery log entry.
/// </summary>
public class WebhookEventLog
{
    /// <summary>
    /// Webhook event identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Webhook event type.
    /// </summary>
    [JsonPropertyName( "type" )]
    public WebhookEventType Type { get; set; }

    /// <summary>
    /// When the webhook event was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Webhook event delivery status.
    /// </summary>
    [JsonPropertyName( "status" )]
    public WebhookEventLogStatus Status { get; set; }
}
