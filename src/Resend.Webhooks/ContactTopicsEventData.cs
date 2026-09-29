using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class ContactTopicsEventData : IWebhookData
{
    /// <summary>
    /// Email address of the contact.
    /// </summary>
    [JsonPropertyName( "email" )]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Topics changed in this update, each with its new subscription.
    /// </summary>
    [JsonPropertyName( "topics" )]
    public List<TopicSubscription> Topics { get; set; } = default!;
}
