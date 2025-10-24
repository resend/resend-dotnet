using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class TemplateVariable
{
    /// <summary>
    /// Type of variable.
    /// </summary>
    [JsonPropertyName( "key" )]
    public string Key { get; set; } = default!;

    /// <summary>
    /// Type of variable.
    /// </summary>
    [JsonPropertyName( "type" )]
    public TemplateVariableType Type { get; set; }

    /// <summary>
    /// Type of variable.
    /// </summary>
    [JsonPropertyName( "fallbackValue" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public object? Default { get; set; }
}