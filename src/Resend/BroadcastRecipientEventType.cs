using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Broadcast recipient event types.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<BroadcastRecipientEventType> ) )]
public enum BroadcastRecipientEventType
{
    /// <summary>
    /// Recipient the broadcast was sent to.
    /// </summary>
    [JsonStringValue( "sent" )]
    Sent,

    /// <summary>
    /// Recipient whose email was delivered.
    /// </summary>
    [JsonStringValue( "delivered" )]
    Delivered,

    /// <summary>
    /// Recipient who opened the email.
    /// </summary>
    [JsonStringValue( "opened" )]
    Opened,

    /// <summary>
    /// Recipient who clicked a link in the email.
    /// </summary>
    [JsonStringValue( "clicked" )]
    Clicked,

    /// <summary>
    /// Recipient whose email bounced.
    /// </summary>
    [JsonStringValue( "bounced" )]
    Bounced,

    /// <summary>
    /// Recipient who marked the email as spam.
    /// </summary>
    [JsonStringValue( "complained" )]
    Complained,

    /// <summary>
    /// Recipient who unsubscribed.
    /// </summary>
    [JsonStringValue( "unsubscribed" )]
    Unsubscribed,

    /// <summary>
    /// Recipient who was suppressed and did not receive the email.
    /// </summary>
    [JsonStringValue( "suppressed" )]
    Suppressed,
}
