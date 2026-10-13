using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// File stored on an inbox draft.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-draft" />
public class InboxDraftAttachment
{
    /// <summary>
    /// Attachment identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Filename of the attachment.
    /// </summary>
    [JsonPropertyName( "filename" )]
    public string Filename { get; set; } = default!;

    /// <summary>
    /// MIME type of the attachment.
    /// </summary>
    [JsonPropertyName( "content_type" )]
    public string ContentType { get; set; } = default!;

    /// <summary>
    /// Size of the attachment, in bytes.
    /// </summary>
    [JsonPropertyName( "size" )]
    public long FileSize { get; set; }
}
