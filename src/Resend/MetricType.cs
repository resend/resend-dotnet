using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A metric that can be requested from <see cref="IResend.EmailMetricsAsync"/>.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<MetricType> ) )]
public enum MetricType
{
    /// <summary>
    /// Number of emails received/accepted for sending.
    /// </summary>
    [JsonStringValue( "received" )]
    Received = 1,

    /// <summary>
    /// Number of emails delivered to the recipient's mail server.
    /// </summary>
    [JsonStringValue( "delivered" )]
    Delivered,

    /// <summary>
    /// Number of emails the recipient marked as spam.
    /// </summary>
    [JsonStringValue( "complained" )]
    Complained,

    /// <summary>
    /// Number of emails suppressed instead of being sent.
    /// </summary>
    [JsonStringValue( "suppressed" )]
    Suppressed,

    /// <summary>
    /// Number of emails that bounced, of any kind.
    /// </summary>
    [JsonStringValue( "bounced" )]
    Bounced,

    /// <summary>
    /// Number of emails that bounced transiently (may succeed on retry).
    /// </summary>
    [JsonStringValue( "bounced_transient" )]
    BouncedTransient,

    /// <summary>
    /// Number of emails that bounced permanently.
    /// </summary>
    [JsonStringValue( "bounced_permanent" )]
    BouncedPermanent,

    /// <summary>
    /// Number of emails that bounced for an undetermined reason.
    /// </summary>
    [JsonStringValue( "bounced_undetermined" )]
    BouncedUndetermined,

    /// <summary>
    /// Number of emails opened by the recipient.
    /// </summary>
    [JsonStringValue( "opened" )]
    Opened,

    /// <summary>
    /// Number of emails whose links were clicked by the recipient.
    /// </summary>
    [JsonStringValue( "clicked" )]
    Clicked,

    /// <summary>
    /// Number of recipients who unsubscribed.
    /// </summary>
    [JsonStringValue( "unsubscribed" )]
    Unsubscribed,

    /// <summary>
    /// Number of emails whose delivery was delayed.
    /// </summary>
    [JsonStringValue( "delivery_delayed" )]
    DeliveryDelayed,

    /// <summary>
    /// Number of emails that failed to send.
    /// </summary>
    [JsonStringValue( "failed" )]
    Failed,

    /// <summary>
    /// Number of emails sent.
    /// </summary>
    [JsonStringValue( "sent" )]
    Sent,

    /// <summary>
    /// Number of unique recipients who opened an email.
    /// </summary>
    [JsonStringValue( "unique_opened" )]
    UniqueOpened,

    /// <summary>
    /// Number of unique recipients who clicked a link.
    /// </summary>
    [JsonStringValue( "unique_clicked" )]
    UniqueClicked,

    /// <summary>
    /// Delivered / sent, as a fraction.
    /// </summary>
    [JsonStringValue( "delivery_rate" )]
    DeliveryRate,

    /// <summary>
    /// Unique opens / delivered, as a fraction.
    /// </summary>
    [JsonStringValue( "open_rate" )]
    OpenRate,

    /// <summary>
    /// Unique clicks / delivered, as a fraction.
    /// </summary>
    [JsonStringValue( "click_rate" )]
    ClickRate,

    /// <summary>
    /// Bounced / sent, as a fraction.
    /// </summary>
    [JsonStringValue( "bounce_rate" )]
    BounceRate,

    /// <summary>
    /// Complained / delivered, as a fraction.
    /// </summary>
    [JsonStringValue( "complaint_rate" )]
    ComplaintRate,

    /// <summary>
    /// Unsubscribed / delivered, as a fraction.
    /// </summary>
    [JsonStringValue( "unsubscribe_rate" )]
    UnsubscribeRate,
}
