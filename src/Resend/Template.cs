using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class Template
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Template identifier.
    /// </summary>
    [JsonPropertyName( "current_version_id" )]
    public Guid CurrentVersionId { get; set; }

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


    /// <summary>
    /// Sender email address.
    /// </summary>
    [JsonPropertyName( "from" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddress? From { get; set; }

    /// <summary>
    /// Email subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Subject { get; set; }

    /// <summary>
    /// Reply-to email address.
    /// </summary>
    [JsonPropertyName( "reply_to" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? ReplyTo { get; set; }

    /// <summary />
    [JsonPropertyName( "html" )]
    public string HtmlBody { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "text" )]
    public string TextBody { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "variables" )]
    public List<TemplateVariable>? Variables { get; set; }

    /// <summary />
    [JsonPropertyName( "has_unpublished_versions" )]
    public bool HasUnpublishedVersions { get; set; }
}