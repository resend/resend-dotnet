using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Draft carried by an inbox webhook event.
/// </summary>
/// <remarks>
/// It has no body. Fetch the body with
/// <see cref="IResend.InboxDraftRetrieveAsync(Guid, Guid, CancellationToken)"/>.
/// </remarks>
public class InboxEventDraft
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
    /// Whether the draft starts a new conversation, or replies to an email.
    /// </summary>
    [JsonPropertyName( "type" )]
    public InboxDraftType DraftType { get; set; }

    /// <summary>
    /// Recipients.
    /// </summary>
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
    /// Identifier of the queued outbound email after send.
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
