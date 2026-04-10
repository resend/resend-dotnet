using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Execution record for one step within an automation run.
/// </summary>
public class AutomationRunStep
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
    /// Step execution status.
    /// </summary>
    [JsonPropertyName( "status" )]
    public string Status { get; set; } = default!;

    /// <summary>
    /// When the step started.
    /// </summary>
    [JsonPropertyName( "started_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentStarted { get; set; }

    /// <summary>
    /// When the step finished, if applicable.
    /// </summary>
    [JsonPropertyName( "completed_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentCompleted { get; set; }

    /// <summary>
    /// Step output payload when present.
    /// </summary>
    [JsonPropertyName( "output" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? Output { get; set; }

    /// <summary>
    /// Error details when the step failed.
    /// </summary>
    [JsonPropertyName( "error" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? Error { get; set; }

    /// <summary>
    /// When the step record was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }
}
