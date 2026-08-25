using System.Text.Json.Serialization;

namespace Resend;

public class WebhookEventAttemptListResult
{
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    [JsonPropertyName( "has_more" )]
    public bool HasMore { get; set; }

    [JsonPropertyName( "data" )]
    public List<WebhookEventAttempt> Data { get; set; } = default!;
}
