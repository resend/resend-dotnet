using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A page of webhook events.
/// </summary>
public class WebhookEventListResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Whether more webhook events are available.
    /// </summary>
    [JsonPropertyName( "has_more" )]
    public bool HasMore { get; set; }

    /// <summary>
    /// Page of webhook events.
    /// </summary>
    [JsonPropertyName( "data" )]
    public List<WebhookEventLog> Data { get; set; } = default!;
}
