using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object to create a webhook.
/// </summary>
public class WebhookAddData
{
    /// <summary>
    /// Webhook endpoint URL.
    /// </summary>
    [JsonPropertyName( "endpoint" )]
    public string EndpointUrl { get; set; } = default!;

    /// <summary>
    /// Supported webhook events.
    /// </summary>
    [JsonPropertyName( "events" )]
    public List<WebhookEventType> Events { get; set; } = default!;
}
