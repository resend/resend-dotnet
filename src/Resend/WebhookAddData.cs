using System.Text.Json.Serialization;

namespace Resend;

public class WebhookAddData
{
    [JsonPropertyName( "endpoint" )]
    public string EndpointUrl { get; set; } = default!;

    [JsonPropertyName( "events" )]
    public List<WebhookEventType> Events { get; set; } = default!;
}
