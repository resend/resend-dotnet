using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to create an automation.
/// </summary>
public class AutomationCreateData
{
    /// <summary>
    /// Display name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Either <c>enabled</c> or <c>disabled</c>. Defaults to <c>disabled</c> when omitted.
    /// </summary>
    [JsonPropertyName( "status" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Status { get; set; }

    /// <summary>
    /// Steps; must include at least one trigger step.
    /// </summary>
    [JsonPropertyName( "steps" )]
    public List<AutomationStepData> Steps { get; set; } = default!;

    /// <summary>
    /// Edges between steps (may be empty for single-step automations).
    /// </summary>
    [JsonPropertyName( "edges" )]
    public List<AutomationEdge> Edges { get; set; } = default!;
}
