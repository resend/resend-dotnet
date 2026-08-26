using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A page of webhook event delivery attempts.
/// </summary>
public class WebhookEventAttemptListResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Whether more delivery attempts are available.
    /// </summary>
    [JsonPropertyName( "has_more" )]
    public bool HasMore { get; set; }

    /// <summary>
    /// Page of webhook event delivery attempts.
    /// </summary>
    [JsonPropertyName( "data" )]
    public List<WebhookEventAttempt> Data { get; set; } = default!;
}
