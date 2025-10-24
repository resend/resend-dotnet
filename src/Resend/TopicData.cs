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
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Description { get; set; }

    /// <summary>
    /// Default subscription.
    /// </summary>
    [JsonPropertyName( "defaultSubscription" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public SubscriptionType? SubscriptionDefault { get; set; }
}