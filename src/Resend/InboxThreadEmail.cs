using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email in an inbox thread.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-thread-email" />
public class InboxThreadEmail
{
    /// <summary>
    /// Email identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Whether the inbox received the email or sent it.
    /// </summary>
    [JsonPropertyName( "direction" )]
    public InboxEmailDirection Direction { get; set; }

    /// <summary>
    /// Sender email address. Includes the display name when the sender has one.
    /// </summary>
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary>
    /// Recipients of the email.
    /// </summary>
    /// <remarks>
    /// For a received email, these are the addresses it was delivered to, not the full
    /// <c>To</c> header.
    /// </remarks>
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = new EmailAddressList();

    /// <summary>
    /// CC recipients of the email.
    /// </summary>
    [JsonPropertyName( "cc" )]
    public EmailAddressList Cc { get; set; } = new EmailAddressList();

    /// <summary>
    /// BCC recipients of the email.
    /// </summary>
    /// <remarks>
    /// Always empty for a received email.
    /// </remarks>
    [JsonPropertyName( "bcc" )]
    public EmailAddressList Bcc { get; set; } = new EmailAddressList();

    /// <summary>
    /// Reply-To addresses of the email.
    /// </summary>
    [JsonPropertyName( "reply_to" )]
    public EmailAddressList ReplyTo { get; set; } = new EmailAddressList();

    /// <summary>
    /// Subject of the email.
    /// </summary>
    [JsonPropertyName( "subject" )]
    public string? Subject { get; set; }

    /// <summary>
    /// Message-ID header of the email.
    /// </summary>
    [JsonPropertyName( "message_id" )]
    public string? MessageId { get; set; }

    /// <summary>
    /// The HTML version of the email.
    /// </summary>
    [JsonPropertyName( "html" )]
    public string? HtmlBody { get; set; }

    /// <summary>
    /// The plain text version of the email.
    /// </summary>
    [JsonPropertyName( "text" )]
    public string? TextBody { get; set; }

    /// <summary>
    /// Attachments of the email.
    /// </summary>
    [JsonPropertyName( "attachments" )]
    public List<InboxThreadEmailAttachment> Attachments { get; set; } = new();

    /// <summary>
    /// Whether the email has been read.
    /// </summary>
    [JsonPropertyName( "read" )]
    public bool IsRead { get; set; }

    /// <summary>
    /// Moment when the inbox received the email, or when the inbox sent it.
    /// </summary>
    [JsonPropertyName( "received_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentReceived { get; set; }
}
