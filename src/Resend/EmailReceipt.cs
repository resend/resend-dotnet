using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Receipt of a sent email.
/// </summary>
public class EmailReceipt
{
    /// <summary>
    /// Email identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Sender email address.
    /// </summary>
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary>
    /// Recipient email address.
    /// </summary>
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = default!;

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
    /// Email subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    public string Subject { get; set; } = default!;


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
    /// Moment for which the email is scheduled for/at.
    /// </summary>
    [JsonPropertyName( "scheduled_at" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTimeOrHuman? MomentSchedule { get; set; }

    /// <summary />
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary />
    [JsonPropertyName( "last_event" )]
    public EmailStatus? LastEvent { get; set; }
}
