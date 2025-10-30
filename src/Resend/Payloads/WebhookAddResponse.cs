using System.Text.Json.Serialization;

namespace Resend.Payloads;

public class WebhookAddResponse
{
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    [JsonPropertyName( "signing_secret" )]
    public string SigningSecret { get; set; } = default!;
}
