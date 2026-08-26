using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Details about a webhook event and its delivery status.
/// </summary>
public class WebhookEventDetails
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

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

    /// <summary>
    /// When the next delivery attempt is scheduled.
    /// </summary>
    [JsonPropertyName( "next_attempt_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentNextAttempt { get; set; }

    /// <summary>
    /// Webhook event payload.
    /// </summary>
    [JsonPropertyName( "payload" )]
    public JsonElement Payload { get; set; }
}
