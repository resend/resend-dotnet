using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Unsent draft on an inbox.
/// </summary>
/// <remarks>
/// Returned when retrieving or updating a single draft. The list endpoint returns
/// <see cref="InboxDraftSummary"/>, which carries a snippet instead of the body.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-draft" />
public class InboxDraft
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

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
    /// The HTML version of the draft.
    /// </summary>
    [JsonPropertyName( "html" )]
    public string? HtmlBody { get; set; }

    /// <summary>
    /// The plain text version of the draft.
    /// </summary>
    [JsonPropertyName( "text" )]
    public string? TextBody { get; set; }

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
    /// Identifier of the outbound email queued when the draft was sent.
    /// </summary>
    /// <remarks>
    /// Null until the draft is sent.
    /// </remarks>
    [JsonPropertyName( "email_id" )]
    public Guid? EmailId { get; set; }

    /// <summary>
    /// Moment when the draft was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment when the draft was last saved.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }
}
