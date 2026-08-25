using System.Text.Json.Serialization;

namespace Resend;

public class WebhookEventAttempt
{
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    [JsonPropertyName( "http_status_code" )]
    public int HttpStatusCode { get; set; }

    [JsonPropertyName( "response" )]
    public string Response { get; set; } = default!;

    [JsonPropertyName( "sent_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentSent { get; set; }
}
