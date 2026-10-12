using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.thread.assigned</c> event.
/// </summary>
public class InboxThreadAssignedEventData : IWebhookData
{
    /// <summary>
    /// Email of the member the thread is now assigned to.
    /// </summary>
    [JsonPropertyName( "assignee_email" )]
    public string? AssigneeEmail { get; set; }

    /// <summary>
    /// Email of the member who made the assignment.
    /// </summary>
    /// <remarks>
    /// Null when no member made it.
    /// </remarks>
    [JsonPropertyName( "assigned_by_email" )]
    public string? AssignedByEmail { get; set; }

    /// <summary>
    /// Email of the member the thread was assigned to before.
    /// </summary>
    /// <remarks>
    /// Null when it was unassigned.
    /// </remarks>
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
