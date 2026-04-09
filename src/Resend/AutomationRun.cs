using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A single automation run with per-step execution details.
/// </summary>
public class AutomationRun
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Run identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Run status.
    /// </summary>
    [JsonPropertyName( "status" )]
    public string Status { get; set; } = default!;

    /// <summary>
    /// When the run started.
    /// </summary>
    [JsonPropertyName( "started_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentStarted { get; set; }

    /// <summary>
    /// When the run finished, if applicable.
    /// </summary>
    [JsonPropertyName( "completed_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentCompleted { get; set; }

    /// <summary>
    /// When the run record was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Per-step execution details.
    /// </summary>
    [JsonPropertyName( "steps" )]
    public List<AutomationRunStep> Steps { get; set; } = default!;
}
