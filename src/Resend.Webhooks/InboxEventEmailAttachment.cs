using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Attachment on an email in an inbox webhook event.
/// </summary>
public class InboxEventEmailAttachment
{
    /// <summary>
    /// Attachment identifier.
    /// </summary>
    /// <remarks>
    /// Not always a UUID. Outbound attachments are identified by their position in the
    /// sent email.
    /// </remarks>
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
