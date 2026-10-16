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
