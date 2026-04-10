using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A step in an automation graph for create and update requests.
/// </summary>
public class AutomationStepData
{
    /// <summary>
    /// Unique reference for this step, used by <see cref="AutomationEdge.From"/> and <see cref="AutomationEdge.To"/>.
    /// </summary>
    [JsonPropertyName( "key" )]
    public string Ref { get; set; } = default!;

    /// <summary>
    /// Step type (for example <c>trigger</c>, <c>send_email</c>, <c>delay</c>).
    /// </summary>
    [JsonPropertyName( "type" )]
    public string Type { get; set; } = default!;

    /// <summary>
    /// Step configuration; shape depends on <see cref="Type"/>.
    /// </summary>
    [JsonPropertyName( "config" )]
    public JsonElement Config { get; set; }
}
