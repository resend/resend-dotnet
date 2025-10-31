using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class TopicData
{
    /// <summary>
    /// Topic name.
    /// </summary>
    /// <remarks>
    /// Required when creating the topic.
    /// </remarks>
    [JsonPropertyName( "name" )]
    public string? Name { get; set; }

    /// <summary>
    /// Topic description.
    /// </summary>
    [JsonPropertyName( "description" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Description { get; set; }

    /// <summary>
    /// Default subscription.
    /// </summary>
    [JsonPropertyName( "default_subscription" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public SubscriptionType? SubscriptionDefault { get; set; }
}