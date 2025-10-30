using System.Text.Json.Serialization;

namespace Resend.Payloads;

public class WebhookRemoveResponse
{
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    [JsonPropertyName( "deleted" )]
    public bool Deleted { get; set; }
}
