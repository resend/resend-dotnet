using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class EmailMessageTemplate
{
    /// <summary>
    /// Identifier of published template.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Template variables.
    /// </summary>
    [JsonPropertyName( "variables" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, object>? Variables { get; set; }
}