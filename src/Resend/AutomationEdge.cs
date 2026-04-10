using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Connection between two automation steps.
/// </summary>
/// <remarks>
/// On create and update, <see cref="From"/> and <see cref="To"/> are step refs.
/// On retrieve, they are internal step identifiers.
/// </remarks>
public class AutomationEdge
{
    /// <summary>
    /// Source step ref or internal id.
    /// </summary>
    [JsonPropertyName( "from" )]
    public string From { get; set; } = default!;

    /// <summary>
    /// Destination step ref or internal id.
    /// </summary>
    [JsonPropertyName( "to" )]
    public string To { get; set; } = default!;

    /// <summary>
    /// Connection type: <c>default</c>, <c>condition_met</c>, <c>condition_not_met</c>, <c>timeout</c>, <c>event_received</c>.
    /// </summary>
    [JsonPropertyName( "type" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? EdgeType { get; set; }
}
