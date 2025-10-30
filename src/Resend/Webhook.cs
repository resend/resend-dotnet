using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class Webhook
{
    /// <summary>
    /// Domain identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Endpoint URL.
    /// </summary>
    [JsonPropertyName( "endpoint" )]
    public string EndpointUrl { get; set; } = default!;

    /// <summary>
    /// Moment in which endpoint was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary />
    [JsonPropertyName( "status" )]
    public WebhookStatus Status { get; set; }

    /// <summary />
    [JsonPropertyName( "events" )]
    public List<WebhookEventType>? Events { get; set; }

    /// <summary />
    /// <remarks>
    /// Only returned in WebookRetrieve.
    /// </remarks>
    [JsonPropertyName( "signing_secret" )]
    public string? SigningSecret { get; set; }
}
