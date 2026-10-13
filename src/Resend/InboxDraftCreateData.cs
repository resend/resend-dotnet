using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to create an inbox draft.
/// </summary>
/// <remarks>
/// At least one of <see cref="To"/>, <see cref="Cc"/>, <see cref="Bcc"/>,
/// <see cref="Subject"/>, <see cref="TextBody"/>, <see cref="HtmlBody"/> or
/// <see cref="Attachments"/> must be non-empty. The combined recipients cannot exceed 50. Omit <see cref="ThreadId"/> and
/// <see cref="ReplyToEmailId"/> for a draft that starts a new thread. Set both together
/// for a reply draft in an existing thread.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/create-draft" />
public class InboxDraftCreateData
{
    /// <summary>
    /// Recipient email address (list).
    /// </summary>
    /// <remarks>
    /// Each value can include a friendly name. A reply draft without recipients is addressed
    /// to the sender of the email it replies to, or to its Reply-To when set. For an email
    /// the inbox sent, it defaults to that email's to recipients. Assign an empty list to
    /// leave it empty.
    /// </remarks>
    [JsonPropertyName( "to" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? To { get; set; }

    /// <summary>
    /// Cc/carbon-copy recipient email address.
    /// </summary>
    [JsonPropertyName( "cc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? Cc { get; set; }

    /// <summary>
    /// Bcc/blind carbon copy recipient email address.
    /// </summary>
    [JsonPropertyName( "bcc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? Bcc { get; set; }

    /// <summary>
    /// Subject.
    /// </summary>
    /// <remarks>
    /// Max 2000 characters. A string that is only whitespace, or that contains a line break,
    /// is rejected.
    /// </remarks>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Subject { get; set; }

    /// <summary>
    /// The HTML version of the draft.
    /// </summary>
    [JsonPropertyName( "html" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? HtmlBody { get; set; }

    /// <summary>
    /// The plain text version of the draft.
    /// </summary>
    [JsonPropertyName( "text" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TextBody { get; set; }

    /// <summary>
    /// Files to store on the draft.
    /// </summary>
    /// <remarks>
    /// Each one needs <see cref="EmailAttachment.Content"/>, <see cref="EmailAttachment.Filename"/>
    /// and <see cref="EmailAttachment.ContentType"/>. A path or a content identifier returns
    /// a 422. The files on a draft can total at most 25 MB. An empty file or a blocked file
    /// type returns a 422.
    /// </remarks>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailAttachment>? Attachments { get; set; }

    /// <summary>
    /// Thread identifier to reply in. Must be set with <see cref="ReplyToEmailId"/>.
    /// </summary>
    [JsonPropertyName( "thread_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? ThreadId { get; set; }

    /// <summary>
    /// Identifier of the email to reply to. Must be set with <see cref="ThreadId"/>.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="InboxThreadEmail.Id"/> returned by
    /// <see cref="IResend.InboxThreadEmailListAsync(Guid, Guid, PaginatedQuery?, CancellationToken)"/>.
    /// </remarks>
    [JsonPropertyName( "reply_to_email_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? ReplyToEmailId { get; set; }
}
