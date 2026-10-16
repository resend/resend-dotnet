using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.thread.labels.updated</c> event.
/// </summary>
/// <remarks>
/// One event carries one label, in either <see cref="Added"/> or <see cref="Removed"/>.
/// </remarks>
public class InboxThreadLabelsUpdatedEventData : IWebhookData
{
    /// <summary>
    /// Labels applied to the thread.
    /// </summary>
    [JsonPropertyName( "added" )]
    public List<InboxThreadLabel> Added { get; set; } = new();

    /// <summary>
    /// Labels removed from the thread.
    /// </summary>
    [JsonPropertyName( "removed" )]
    public List<InboxThreadLabel> Removed { get; set; } = new();

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
