using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.draft.created</c>, <c>inbox.draft.updated</c> or
/// <c>inbox.draft.deleted</c> event.
/// </summary>
public class InboxDraftEventData : IWebhookData
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
    /// Thread identifier.
    /// </summary>
    /// <remarks>
    /// Only on reply drafts.
    /// </remarks>
    [JsonPropertyName( "thread_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? ThreadId { get; set; }

    /// <summary>
    /// Draft identifier.
    /// </summary>
    [JsonPropertyName( "draft_id" )]
    public Guid DraftId { get; set; }

    /// <summary>
    /// Thread as it is when the webhook is sent.
    /// </summary>
    /// <remarks>
    /// Only on reply drafts. Null when the thread no longer exists.
    /// </remarks>
    [JsonPropertyName( "thread" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public InboxThreadSummary? Thread { get; set; }

    /// <summary>
    /// Draft as it is when the webhook is sent, without a body.
    /// </summary>
    /// <remarks>
    /// Fetch the body with <see cref="IResend.InboxDraftRetrieveAsync(InboxIdOrEmail, Guid, CancellationToken)"/>.
    /// Null when the draft no longer exists.
    /// </remarks>
    [JsonPropertyName( "draft" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public InboxEventDraft? Draft { get; set; }
}
