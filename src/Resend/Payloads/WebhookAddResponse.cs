using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary>
/// Request object to create a Webhook.
/// </summary>
public class WebhookAddResponse
{
    /// <summary>
    /// Object type.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Object identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Webhook's unique identifier.
    /// </summary>
    [JsonPropertyName( "signing_secret" )]
    public string SigningSecret { get; set; } = default!;
}
