using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Payload used to create/update a webhook.
/// </summary>
public class WebhookData
{
    /// <summary>
    /// Endpoint URL.
    /// </summary>
    [JsonPropertyName( "endpoint" )]
    public string EndpointUrl { get; set; } = default!;

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
}