using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Automation summary returned when listing automations.
/// </summary>
public class AutomationSummary
{
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
}
