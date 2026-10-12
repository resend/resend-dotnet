using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Data of an <c>inbox.thread.created</c>, <c>inbox.email.received</c> or
/// <c>inbox.email.sent</c> event.
/// </summary>
public class InboxThreadEmailEventData : IWebhookData
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
    [JsonPropertyName( "thread_id" )]
    public Guid ThreadId { get; set; }

    /// <summary>
    /// Email identifier.
    /// </summary>
    [JsonPropertyName( "email_id" )]
    public Guid EmailId { get; set; }

    /// <summary>
    /// Thread as it is when the webhook is sent.
    /// </summary>
    /// <remarks>
    /// Null when the thread no longer exists.
    /// </remarks>
    [JsonPropertyName( "thread" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public InboxThreadSummary? Thread { get; set; }

    /// <summary>
    /// Email as it is when the webhook is sent, without a body.
    /// </summary>
    /// <remarks>
    /// Fetch the body with <see cref="IResend.InboxThreadEmailRetrieveAsync(Guid, Guid, Guid, CancellationToken)"/>.
    /// Null when the email no longer exists.
    /// </remarks>
    [JsonPropertyName( "email" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public InboxEventEmail? Email { get; set; }
}
