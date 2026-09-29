using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class TopicEventData : IWebhookData
{
    /// <summary>
    /// Topic identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Topic name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Topic description.
    /// </summary>
    [JsonPropertyName( "description" )]
    public string? Description { get; set; }

    /// <summary>
    /// Default subscription.
    /// </summary>
    [JsonPropertyName( "default_subscription" )]
    public SubscriptionType SubscriptionDefault { get; set; }

    /// <summary>
    /// Whether the topic has been deleted.
    /// </summary>
    [JsonPropertyName( "deleted" )]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Moment when the topic was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment when the topic was last updated.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }
}
