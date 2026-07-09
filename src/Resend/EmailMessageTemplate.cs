using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class EmailMessageTemplate
{
    /// <summary>
    /// Identifier or alias of the published template.
    /// </summary>
    [JsonPropertyName( "id" )]
    public string TemplateId { get; set; } = default!;

    /// <summary>
    /// Template variables.
    /// </summary>
    [JsonPropertyName( "variables" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, object>? Variables { get; set; }
}