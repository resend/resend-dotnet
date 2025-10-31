using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class Webhook
{
    /// <summary>
    /// Domain identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Endpoint URL.
    /// </summary>
    [JsonPropertyName( "endpoint" )]
    public string EndpointUrl { get; set; } = default!;

    /// <summary>
    /// Moment in which endpoint was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Whether webhook is enabled or not.
    /// </summary>
    [JsonPropertyName( "status" )]
    public WebhookStatus Status { get; set; }

    /// <summary>
    /// List of events which are emitted to the webhook.
    /// </summary>
    [JsonPropertyName( "events" )]
    public List<WebhookEventType> Events { get; set; } = default!;

    /// <summary>
    /// Signing secret, used to validate the webhook signature.
    /// </summary>
    /// <remarks>
    /// Not returned in <code>WebhookListAsync</code>.
    /// </remarks>
    [JsonPropertyName( "signing_secret" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? SigningSecret { get; set; }
}