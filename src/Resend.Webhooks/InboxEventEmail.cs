using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Email carried by an inbox webhook event.
/// </summary>
/// <remarks>
/// It has no body. Fetch the body with
/// <see cref="IResend.InboxThreadEmailRetrieveAsync(Guid, Guid, Guid, CancellationToken)"/>.
/// </remarks>
public class InboxEventEmail
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
    /// Sender, as an address or as <c>Name &lt;address&gt;</c>.
    /// </summary>
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary>
    /// Recipients of the email.
    /// </summary>
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
    [JsonPropertyName( "bcc" )]
    public EmailAddressList Bcc { get; set; } = new EmailAddressList();

    /// <summary>
    /// Reply-To addresses.
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
    /// Attachments on the email.
    /// </summary>
    [JsonPropertyName( "attachments" )]
    public List<InboxEventEmailAttachment> Attachments { get; set; } = new();

    /// <summary>
    /// Whether the email has been read.
    /// </summary>
    [JsonPropertyName( "read" )]
    public bool IsRead { get; set; }

    /// <summary>
    /// Moment when the email arrived or was sent.
    /// </summary>
    [JsonPropertyName( "received_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentReceived { get; set; }
}
