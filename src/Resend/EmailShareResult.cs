using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Result of creating a shareable link for a sent or received email.
/// </summary>
public class EmailShareResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Email identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Shareable link URL.
    /// </summary>
    [JsonPropertyName( "url" )]
    public string Url { get; set; } = default!;
}
