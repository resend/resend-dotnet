using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email attachment.
/// </summary>
public class EmailAttachment
{
    /// <summary>
    /// Name of the attached file.
    /// </summary>
    [JsonPropertyName( "filename" )]
    public string Filename { get; set; } = default!;

    /// <summary>
    /// Path where the attachment file is hosted. This resource must be publicly
    /// available from Resend servers.
    /// </summary>
    /// <remarks>
    /// One of <see cref="Path"/> or <see cref="Content"/> must be set.
    /// </remarks>
    [JsonPropertyName( "path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Path { get; set; }

    /// <summary>
    /// Content of the attached file.
    /// </summary>
    [JsonPropertyName( "content" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public ByteArrayOrString? Content { get; set; }

    /// <summary>
    /// Content type for the attachment, if not set will be derived from the filename property.
    /// </summary>
    [JsonPropertyName( "content_type" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ContentType { get; set; }



    /// <summary>
    /// Creates an attachment from a local file.
    /// </summary>
    /// <param name="filename">Path to local file.</param>
    /// <returns>Attachment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the file does not exist.</exception>
    public static EmailAttachment From( string filename )
    {
        if ( File.Exists( filename ) == false )
            throw new InvalidOperationException( $"EA001: File '{filename}' does not exist" );

        var attachment = new EmailAttachment();
        attachment.Filename = System.IO.Path.GetFileName( filename );
        attachment.Content = File.ReadAllBytes( filename );

        return attachment;
    }


    /// <summary>
    /// Creates an attachment from a local file.
    /// </summary>
    /// <param name="filename">Path to local file.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Attachment.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the file does not exist.</exception>
    public static async ValueTask<EmailAttachment> FromAsync( string filename, CancellationToken cancellationToken = default )
    {
        if ( File.Exists( filename ) == false )
            throw new InvalidOperationException( $"EA002: File '{filename}' does not exist" );

        var attachment = new EmailAttachment();
        attachment.Filename = System.IO.Path.GetFileName( filename );
        attachment.Content = await File.ReadAllBytesAsync( filename, cancellationToken );

        return attachment;
    }
}