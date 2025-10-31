using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class SegmentData
{
    /// <summary>
    /// Segment name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;
}
