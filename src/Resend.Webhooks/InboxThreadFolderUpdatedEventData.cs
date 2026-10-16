using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.thread.folder.updated</c> event.
/// </summary>
public class InboxThreadFolderUpdatedEventData : IWebhookData
{
    /// <summary>
    /// Folder the thread was in.
    /// </summary>
    [JsonPropertyName( "from" )]
    public InboxFolder From { get; set; }

    /// <summary>
    /// Folder the thread moved to.
    /// </summary>
    [JsonPropertyName( "to" )]
    public InboxFolder To { get; set; }

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
    /// Thread identifier.
    /// </summary>
    [JsonPropertyName( "thread_id" )]
    public Guid ThreadId { get; set; }

    /// <summary>
    /// Thread as it is when the webhook is sent.
    /// </summary>
    /// <remarks>
    /// Null when the thread no longer exists.
    /// </remarks>
    [JsonPropertyName( "thread" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public InboxThreadSummary? Thread { get; set; }
}
