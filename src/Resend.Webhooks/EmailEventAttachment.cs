using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class EmailEventAttachment
{
    /// <summary />
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "content_type" )]
    public string ContentType { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "filename" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Filename { get; set; }

    /// <summary />
    [JsonPropertyName( "content_disposition" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ContentDisposition { get; set; }

    /// <summary />
    [JsonPropertyName( "content_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ContentId { get; set; }
}
