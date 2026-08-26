using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A delivery attempt for a webhook event.
/// </summary>
public class WebhookEventAttempt
{
    /// <summary>
    /// Delivery attempt identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    /// <summary>
    /// HTTP status code returned by the webhook endpoint.
    /// </summary>
    [JsonPropertyName( "http_status_code" )]
    public int HttpStatusCode { get; set; }

    /// <summary>
    /// Response returned by the webhook endpoint.
    /// </summary>
    [JsonPropertyName( "response" )]
    public string Response { get; set; } = default!;

    /// <summary>
    /// When the delivery attempt was sent.
    /// </summary>
    [JsonPropertyName( "sent_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentSent { get; set; }
}
