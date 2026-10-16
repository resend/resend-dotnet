using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary />
public class InboxThreadLabelsRequest
{
    /// <summary />
    [JsonPropertyName( "labels" )]
    public List<Guid> Labels { get; set; } = default!;
}
