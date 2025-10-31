using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class ReceivedEmail
{
    /// <summary>
    /// Received email identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Sender email address.
    /// </summary>
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary>
    /// Recipient email address (list).
    /// </summary>
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = new EmailAddressList();

    /// <summary>
    /// Email subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    public string Subject { get; set; } = default!;

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
    /// Reply-to email address.
    /// </summary>
    [JsonPropertyName( "reply_to" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? ReplyTo { get; set; }

    /// <summary>
    /// Message identifier.
    /// </summary>
    [JsonPropertyName( "message_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? MessageId { get; set; }


    /// <summary>
    /// The plain text version of the message.
    /// </summary>
    [JsonPropertyName( "text" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TextBody { get; set; }

    /// <summary>
    /// The HTML version of the message.
    /// </summary>
    [JsonPropertyName( "html" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? HtmlBody { get; set; }


    /// <summary>
    /// Attachments.
    /// </summary>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<ReceivedEmailAttachment>? Attachments { get; set; }

    /// <summary>
    /// Moment in which the email was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTime? MomentCreated { get; set; }
}