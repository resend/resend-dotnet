using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A step returned when retrieving an automation.
/// </summary>
public class AutomationStep
{
    /// <summary>
    /// Unique key identifying this step within the automation graph.
    /// </summary>
    [JsonPropertyName( "key" )]
    public string? Key { get; set; }

    /// <summary>
    /// Step type.
    /// </summary>
    [JsonPropertyName( "type" )]
    public string Type { get; set; } = default!;

    /// <summary>
    /// Step configuration; shape depends on <see cref="Type"/>.
    /// </summary>
    [JsonPropertyName( "config" )]
    public JsonElement Config { get; set; }
}
