using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class SentEmailAttachment
{
    /// <summary>
    /// Sent email attachment identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Attachment filename.
    /// </summary>
    [JsonPropertyName( "filename" )]
    public string Filename { get; set; } = default!;

    /// <summary>
    /// Inline content identifier.
    /// </summary>
    /// <remarks>
    /// Value can then be used as reference in HTML body using <code>cid</code>
    /// scheme, e.g. <code>&lt;img src="cid:property-value" &gt; /></code>.
    /// </remarks>
    [JsonPropertyName( "content_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ContentId { get; set; }

    /// <summary>
    /// MIME content type.
    /// </summary>
    [JsonPropertyName( "content_type" )]
    public string ContentType { get; set; } = default!;

    /// <summary>
    /// Content disposition.
    /// </summary>
    [JsonPropertyName( "content_disposition" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public AttachmentContentDisposition? ContentDisposition { get; set; }

    /// <summary>
    /// Attachment file size.
    /// </summary>
    [JsonPropertyName( "size" )]
    public long? FileSize { get; set; }

    /// <summary>
    /// URL to download attachment.
    /// </summary>
    [JsonPropertyName( "download_url" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? DownloadUrl { get; set; }

    /// <summary>
    /// Moment in which the email attachment shall expire.
    /// </summary>
    [JsonPropertyName( "expires_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTime? MomentExpiration { get; set; }
}