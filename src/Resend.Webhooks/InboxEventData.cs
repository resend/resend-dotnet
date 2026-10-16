using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.created</c> or <c>inbox.updated</c> event.
/// </summary>
public class InboxEventData : IWebhookData
{
    /// <summary>
    /// What made the change.
    /// </summary>
    [JsonPropertyName( "source" )]
    public InboxEventSource Source { get; set; }

    /// <summary>
    /// Inbox identifier.
    /// </summary>
    [JsonPropertyName( "inbox_id" )]
    public Guid InboxId { get; set; }

    /// <summary>
    /// Inbox as it is when the webhook is sent.
    /// </summary>
    /// <remarks>
    /// Null when the inbox no longer exists.
    /// </remarks>
    [JsonPropertyName( "inbox" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Inbox? Inbox { get; set; }
}
