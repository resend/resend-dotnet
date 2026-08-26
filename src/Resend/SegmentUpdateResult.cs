using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Result of updating a segment.
/// </summary>
public class SegmentUpdateResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Segment identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Segment name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;
}
