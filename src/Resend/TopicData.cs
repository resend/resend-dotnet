using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class TopicData
{
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
    [JsonPropertyName( "defaultSubscription" )]
    public SubscriptionType? SubscriptionDefault { get; set; }
}