using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an automation. Provide at least one field; when updating the graph,
/// send <see cref="Steps"/> and <see cref="Edges"/> together.
/// </summary>
public class AutomationUpdateData
{
    /// <summary>
    /// Display name.
    /// </summary>
    [JsonPropertyName( "name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Name { get; set; }

    /// <summary>
    /// Either <c>enabled</c> or <c>disabled</c>.
    /// </summary>
    [JsonPropertyName( "status" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Status { get; set; }

    /// <summary>
    /// Replacement step list when updating the graph.
    /// </summary>
    [JsonPropertyName( "steps" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<AutomationStepData>? Steps { get; set; }

    /// <summary>
    /// Replacement edge list when updating the graph.
    /// </summary>
    [JsonPropertyName( "edges" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<AutomationEdge>? Edges { get; set; }
}
