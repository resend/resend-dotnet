using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class Topic
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
    public string Description { get; set; } = default!;

    /// <summary>
    /// Default subscription.
    /// </summary>
    [JsonPropertyName( "default_subscription" )]
    public SubscriptionType SubscriptionDefault { get; set; }

    /// <summary>
    /// Moment when topic was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }
}