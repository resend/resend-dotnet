using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to reply to an email in an inbox thread.
/// </summary>
/// <remarks>
/// At least one of <see cref="HtmlBody"/> or <see cref="TextBody"/> must contain more than
/// whitespace.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/reply-thread-email" />
public class InboxThreadEmailReplyData
{
    /// <summary>
    /// Cc/carbon-copy recipient email address.
    /// </summary>
    /// <remarks>
    /// Without <see cref="ReplyAll"/>, the reply copies only these addresses. With
    /// <see cref="ReplyAll"/>, these addresses are merged with the copied recipients,
    /// without duplicates. <see cref="Cc"/> and <see cref="Bcc"/> combined must be fewer
    /// than 50 addresses. After the to recipients are added, the reply cannot exceed 50
    /// recipients.
    /// </remarks>
    [JsonPropertyName( "cc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? Cc { get; set; }

    /// <summary>
    /// Bcc/blind carbon copy recipient email address.
    /// </summary>
    /// <remarks>
    /// BCC recipients of the original message are never copied.
    /// </remarks>
    [JsonPropertyName( "bcc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? Bcc { get; set; }

    /// <summary>
    /// The HTML version of the reply.
    /// </summary>
    [JsonPropertyName( "html" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? HtmlBody { get; set; }

    /// <summary>
    /// The plain text version of the reply.
    /// </summary>
    [JsonPropertyName( "text" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TextBody { get; set; }

    /// <summary>
    /// Subject of the reply.
    /// </summary>
    /// <remarks>
    /// Cannot contain a line break. When omitted, the thread subject is used, prefixed with
    /// <c>Re:</c> if it does not already start with it.
    /// </remarks>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Subject { get; set; }

    /// <summary>
    /// Reply to everyone on the original message.
    /// </summary>
    /// <remarks>
    /// For a message the inbox received, the reply goes to its Reply-To addresses, or to its
    /// sender when it has none, and copies the other addresses of its To header and its cc
    /// recipients in cc. For a message the inbox sent, the reply goes to that message's
    /// Reply-To addresses when they are not the inbox address, otherwise to its to
    /// recipients, and copies its cc recipients. The inbox address is always excluded, and
    /// duplicates are removed. Defaults server-side to false.
    /// </remarks>
    [JsonPropertyName( "reply_all" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? ReplyAll { get; set; }

    /// <summary>
    /// Custom headers to add to the email.
    /// </summary>
    /// <remarks>
    /// The In-Reply-To, References and Reply-To headers are set by Resend and cannot be set
    /// here. Sending one of them, in any letter case, returns a 422.
    /// </remarks>
    [JsonPropertyName( "headers" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Files to attach, as when sending an email.
    /// </summary>
    /// <remarks>
    /// Each one needs content or a path. Up to 10 files. The content of all files can total
    /// at most 25 MB. A path file counts toward the 10 files only, because it is fetched
    /// when the email is sent.
    /// </remarks>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailAttachment>? Attachments { get; set; }
}
