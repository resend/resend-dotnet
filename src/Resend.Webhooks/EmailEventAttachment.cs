using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class EmailEventAttachment
{
    /// <summary />
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary />
    [JsonPropertyName( "filename" )]
    public string Filename { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "content_type" )]
    public string ContentType { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "content_disposition" )]
    public string ContentDisposition { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "content_id" )]
    public string ContentId { get; set; } = default!;
}
