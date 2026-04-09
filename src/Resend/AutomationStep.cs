using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A step returned when retrieving an automation (responses omit per-step <c>ref</c>).
/// </summary>
public class AutomationStep
{
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
