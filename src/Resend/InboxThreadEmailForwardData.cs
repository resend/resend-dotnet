using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to forward an email in an inbox thread.
/// </summary>
/// <remarks>
/// <see cref="To"/>, <see cref="Cc"/> and <see cref="Bcc"/> combined cannot exceed 50
/// recipients.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/forward-thread-email" />
public class InboxThreadEmailForwardData
{
    /// <summary>
    /// Recipient email address (list).
    /// </summary>
    /// <remarks>
    /// Must name at least one address.
    /// </remarks>
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = new EmailAddressList();

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
    /// HTML note sent above the quoted original.
    /// </summary>
    [JsonPropertyName( "html" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? HtmlBody { get; set; }

    /// <summary>
    /// Plain text note sent above the quoted original.
    /// </summary>
    [JsonPropertyName( "text" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TextBody { get; set; }

    /// <summary>
    /// Subject of the forward.
    /// </summary>
    /// <remarks>
    /// Cannot contain a line break. When omitted, the thread subject is used, prefixed with
    /// <c>Fwd:</c> if it does not already start with it.
    /// </remarks>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Subject { get; set; }

    /// <summary>
    /// Custom headers to add to the email.
    /// </summary>
    /// <remarks>
    /// The In-Reply-To, References and Reply-To headers are set by Resend and cannot be set
    /// here. Sending one of them, in any letter case, returns a 422. A header name cannot be
    /// empty, and names and values cannot contain line breaks or null characters, or the
    /// request returns a 422.
    /// </remarks>
    [JsonPropertyName( "headers" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Files to attach, as when sending an email.
    /// </summary>
    /// <remarks>
    /// Each one needs content or a path. They share one limit with the attachments of the
    /// original message, which the forward also sends. Together they can have up to 10
    /// files and at most 25 MB of content. A path file counts toward the 10 files only,
    /// because it is fetched when the email is sent. An original attachment that was sent
    /// from a URL cannot be forwarded, and the request returns a 422. An empty file, a
    /// blocked file type, or a path that is not an http or https URL, or that points to
    /// localhost, returns a 422.
    /// </remarks>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailAttachment>? Attachments { get; set; }
}
