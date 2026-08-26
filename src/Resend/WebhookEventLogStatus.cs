using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Webhook event delivery status.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<WebhookEventLogStatus> ) )]
public enum WebhookEventLogStatus
{
    /// <summary>
    /// The webhook event is waiting to be delivered.
    /// </summary>
    [JsonStringValue( "pending" )]
    Pending = 1,

    /// <summary>
    /// Delivery of the webhook event is in progress.
    /// </summary>
    [JsonStringValue( "attempting" )]
    Attempting,

    /// <summary>
    /// The webhook event was delivered successfully.
    /// </summary>
    [JsonStringValue( "success" )]
    Success,

    /// <summary>
    /// Delivery of the webhook event failed.
    /// </summary>
    [JsonStringValue( "failed" )]
    Failed,
}
