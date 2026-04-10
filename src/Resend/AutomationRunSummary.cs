using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A single automation run in a list response.
/// </summary>
public class AutomationRunSummary
{
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
    public DateTime? MomentStarted { get; set; }

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
}
