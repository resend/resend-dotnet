using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class BroadcastData
{
    /// <summary>
    /// Display name of the broadcast. Only used for internal reference.
    /// </summary>
    [JsonPropertyName( "name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Segment identifier.
    /// </summary>
    [JsonPropertyName( "segment_id" )]
    public Guid SegmentId { get; set; }

    /// <summary>
    /// Sender email address.
    /// </summary>
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary>
    /// Email subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    public string Subject { get; set; } = default!;

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
    /// Topic identifier.
    /// </summary>
    [JsonPropertyName( "topic_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? TopicId { get; set; }

    /// <summary>
    /// Send the broadcast immediately after creation.
    /// </summary>
    [JsonPropertyName( "send" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? SendAfterCreate { get; set; }

    /// <summary>
    /// Schedule the broadcast to be sent later.
    /// </summary>
    /// <remarks>
    /// Requires <see cref="SendAfterCreate" /> to be True.
    /// </remarks>
    [JsonPropertyName( "scheduled_at" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTimeOrHuman? MomentSchedule { get; set; }
}