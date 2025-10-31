using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Response when the webhook is created.
/// </summary>
public class WebhookNew
{
    /// <summary>
    /// Identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Signing secret, used to validate the webhook signature.
    /// </summary>
    [JsonPropertyName( "signing_secret" )]
    public string SigningSecret { get; set; } = default!;
}