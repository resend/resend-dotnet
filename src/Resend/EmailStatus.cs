using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email statuses.
/// </summary>
/// <see href="https://github.com/resend/resend-node/blob/canary/src/emails/interfaces/get-email-options.interface.ts" />
[JsonConverter( typeof( JsonStringEnumValueConverter<EmailStatus> ) )]
public enum EmailStatus
{
    /// <summary>
    /// Email bounced (for whatever reason).
    /// </summary>
    [JsonStringValue( "bounced" )]
    Bounced,

    /// <summary>
    /// Email has been canceled.
    /// </summary>
    [JsonStringValue( "canceled" )]
    Canceled,

    /// <summary>
    /// Links in the email have been clicked on by recipient.
    /// </summary>
    /// <remarks>
    /// This status is only available if the 'Click Tracking' option has
    /// been enabled for the domain.
    /// </remarks>
    [JsonStringValue( "clicked" )]
    Clicked,

    /// <summary>
    /// Recipient has filed a complaint.
    /// </summary>
    [JsonStringValue( "complained" )]
    Complained,

    /// <summary>
    /// Email has been delivered to recipient.
    /// </summary>
    [JsonStringValue( "delivered" )]
    Delivered,

    /// <summary>
    /// Email 
    /// </summary>
    [JsonStringValue( "delivery_delayed" )]
    DeliveryDelayed,

    /// <summary>
    /// Email has been opened by recipient.
    /// </summary>
    /// <remarks>
    /// This status is only available if the 'Open Tracking' option has
    /// been enabled for the domain.
    /// </remarks>
    [JsonStringValue( "opened" )]
    Opened,

    /// <summary>
    /// Email has been queued for delivery.
    /// </summary>
    [JsonStringValue( "queued" )]
    Queued,

    /// <summary>
    /// Email is scheduled for (future) delivery.
    /// </summary>
    [JsonStringValue( "scheduled" )]
    Scheduled,

    /// <summary>
    /// Email delivery failed.
    /// </summary>
    [JsonStringValue( "failed" )]
    Failed,
}
