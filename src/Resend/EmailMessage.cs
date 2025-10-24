using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email to be sent.
/// </summary>
public class EmailMessage
{
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
    /// Custom headers to add to the email.
    /// </summary>
    [JsonPropertyName( "headers" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Filename and content of attachments (max 40mb per email).
    /// </summary>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailAttachment>? Attachments { get; set; }

    /// <summary>
    /// Email tags.
    /// </summary>
    [JsonPropertyName( "tags" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailTag>? Tags { get; set; }

    /// <summary>
    /// Email template and variables.
    /// </summary>
    /// <remarks>
    /// Use of a template is mutually exclusive with setting <see cref="HtmlBody" /> or
    /// <see cref="TextBody" />.
    /// </remarks>
    [JsonPropertyName( "template" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailMessageTemplate? Template { get; set; }

    /// <summary>
    /// Moment for which the email is scheduled for/at.
    /// </summary>
    [JsonPropertyName( "scheduled_at" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTimeOrHuman? MomentSchedule { get; set; }

    /// <summary>
    /// Moment in which the email was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTime? MomentCreated { get; set; }
}