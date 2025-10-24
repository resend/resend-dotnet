using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class TemplateSummary
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Alias
    /// </summary>
    [JsonPropertyName( "alias" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Alias { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName( "status" )]
    public TemplateStatus Status { get; set; }

    /// <summary>
    /// Moment when template was published.
    /// </summary>
    [JsonPropertyName( "published_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTime? MomentPublished { get; set; }

    /// <summary>
    /// Moment when template was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment when template was last updated.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }
}