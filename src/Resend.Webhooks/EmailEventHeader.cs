using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Custom header included on an email event.
/// </summary>
public class EmailEventHeader
{
    /// <summary />
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "value" )]
    public string Value { get; set; } = default!;
}
