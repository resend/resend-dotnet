using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object to update a webhook.
/// </summary>
public class WebhookUpdateData
{
    /// <summary>
    /// Webhook endpoint URL.
    /// </summary>
    [JsonPropertyName( "endpoint" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? EndpointUrl { get; set; }

    /// <summary>
    /// Supported webhook events.
    /// </summary>
    [JsonPropertyName( "events" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<WebhookEventType>? Events { get; set; }

    /// <summary>
    /// Webhook status.
    /// </summary>
    [JsonPropertyName( "status" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public WebhookStatus? Status { get; set; }
}
