using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class TopicSubscription
{
    /// <summary>
    /// Topic subscription identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Subscription type.
    /// </summary>
    [JsonPropertyName( "subscription" )]
    public SubscriptionType Subscription { get; set; }
}