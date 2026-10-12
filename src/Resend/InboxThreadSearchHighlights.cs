using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Text fragments where a search query matched, with each match wrapped in <c>**</c>.
/// </summary>
/// <remarks>
/// Only the fields with a match are set. The others are null.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/search-threads" />
public class InboxThreadSearchHighlights
{
    /// <summary>
    /// Matches in the subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? Subject { get; set; }

    /// <summary>
    /// Matches in the body.
    /// </summary>
    [JsonPropertyName( "body" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? Body { get; set; }

    /// <summary>
    /// Matches in the sender.
    /// </summary>
    [JsonPropertyName( "from" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? From { get; set; }

    /// <summary>
    /// Matches in the recipients.
    /// </summary>
    [JsonPropertyName( "to" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? To { get; set; }

    /// <summary>
    /// Matches in the CC recipients.
    /// </summary>
    [JsonPropertyName( "cc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? Cc { get; set; }

    /// <summary>
    /// Matches in the BCC recipients.
    /// </summary>
    [JsonPropertyName( "bcc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? Bcc { get; set; }

    /// <summary>
    /// Matches in the attachment names.
    /// </summary>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? Attachments { get; set; }
}
