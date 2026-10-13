using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Unsent draft, as returned by the list endpoint.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/list-drafts" />
public class InboxDraftSummary
{
    /// <summary>
    /// Draft identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Whether the draft replies to an email in an existing thread, or starts a new thread.
    /// </summary>
    [JsonPropertyName( "type" )]
    public InboxDraftType DraftType { get; set; }

    /// <summary>
    /// Recipients.
    /// </summary>
    /// <remarks>
    /// Null or empty when no recipients are set.
    /// </remarks>
    [JsonPropertyName( "to" )]
    public EmailAddressList? To { get; set; }

    /// <summary>
    /// CC recipients.
    /// </summary>
    [JsonPropertyName( "cc" )]
    public EmailAddressList Cc { get; set; } = new EmailAddressList();

    /// <summary>
    /// BCC recipients.
    /// </summary>
    [JsonPropertyName( "bcc" )]
    public EmailAddressList Bcc { get; set; } = new EmailAddressList();

    /// <summary>
    /// Subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    public string? Subject { get; set; }

    /// <summary>
    /// Short preview of the body.
    /// </summary>
    [JsonPropertyName( "snippet" )]
    public string? Snippet { get; set; }

    /// <summary>
    /// Files stored on the draft.
    /// </summary>
    [JsonPropertyName( "attachments" )]
    public List<InboxDraftAttachment> Attachments { get; set; } = new();

    /// <summary>
    /// Thread identifier, when the draft is a reply.
    /// </summary>
    [JsonPropertyName( "thread_id" )]
    public Guid? ThreadId { get; set; }

    /// <summary>
    /// Identifier of the email the draft replies to, when the draft is a reply.
    /// </summary>
    [JsonPropertyName( "reply_to_email_id" )]
    public Guid? ReplyToEmailId { get; set; }

    /// <summary>
    /// Moment when the draft was last saved.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }
}
