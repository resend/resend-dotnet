using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Attachment of an email in an inbox thread.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-thread-email" />
public class InboxThreadEmailAttachment
{
    /// <summary>
    /// Attachment identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Filename of the attachment.
    /// </summary>
    [JsonPropertyName( "filename" )]
    public string? Filename { get; set; }

    /// <summary>
    /// Size of the attachment, in bytes.
    /// </summary>
    [JsonPropertyName( "size" )]
    public long? FileSize { get; set; }
}
