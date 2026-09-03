using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Result of replaying a webhook event.
/// </summary>
public class WebhookEventReplayResult
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
}
