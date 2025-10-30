using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary>
/// Request object to remove a Webhook.
/// </summary>
public class WebhookRemoveResponse
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
    /// Delete confirmation.
    /// </summary>
    [JsonPropertyName( "deleted" )]
    public bool Deleted { get; set; }
}
