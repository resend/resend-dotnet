using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Full automation resource including graph details.
/// </summary>
public class Automation
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Automation identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Display name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Either <c>enabled</c> or <c>disabled</c>.
    /// </summary>
    [JsonPropertyName( "status" )]
    public string Status { get; set; } = default!;

    /// <summary>
    /// When the automation was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// When the automation was last updated.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }

    /// <summary>
    /// Steps in execution order as stored by the API.
    /// </summary>
    [JsonPropertyName( "steps" )]
    public List<AutomationStep> Steps { get; set; } = default!;

    /// <summary>
    /// Edges connecting steps.
    /// </summary>
    [JsonPropertyName( "edges" )]
    public List<AutomationEdge> Edges { get; set; } = default!;
}
