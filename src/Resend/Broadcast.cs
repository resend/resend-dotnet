using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class Broadcast
{
    /// <summary>
    /// Broadcast identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Display name of the broadcast. Only used for internal reference.
    /// </summary>
    [JsonPropertyName( "name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Audience identifier.
    /// </summary>
    [JsonPropertyName( "audience_id" )]
    public Guid AudienceId { get; set; }

    /// <summary>
    /// From.
    /// </summary>
    /// <remarks>
    /// Set when retrieving the broadcast, null when listing broadcasts.
    /// </remarks>
    [JsonPropertyName( "from" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddress? From { get; set; }

    /// <summary>
    /// Reply-to email address.
    /// </summary>
    /// <remarks>
    /// Set when retrieving the broadcast, null when listing broadcasts.
    /// </remarks>
    [JsonPropertyName( "reply_to" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? ReplyTo { get; set; }

    /// <summary>
    /// Email subject.
    /// </summary>
    /// <remarks>
    /// Set when retrieving the broadcast, null when listing broadcasts.
    /// </remarks>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Subject { get; set; }

    /// <summary>
    /// Preview text.
    /// </summary>
    /// <remarks>
    /// Set when retrieving the broadcast, null when listing broadcasts.
    /// </remarks>
    [JsonPropertyName( "preview_text" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? PreviewText { get; set; }

    /// <summary>
    /// Status.
    /// </summary>
    [JsonPropertyName( "status" )]
    public BroadcastStatus Status { get; set; }

    /// <summary>
    /// Moment in which the Broadcast was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment in which the Broadcast is/was scheduled for.
    /// </summary>
    [JsonPropertyName( "scheduled_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentScheduled { get; set; }

    /// <summary>
    /// Moment in which the Broadcast was sent.
    /// </summary>
    [JsonPropertyName( "sent_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentSent { get; set; }
}