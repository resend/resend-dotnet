using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.thread.unassigned</c> event.
/// </summary>
public class InboxThreadUnassignedEventData : IWebhookData
{
    /// <summary>
    /// Email of the member the thread was assigned to.
    /// </summary>
    [JsonPropertyName( "previous_assignee_email" )]
    public string? PreviousAssigneeEmail { get; set; }

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
