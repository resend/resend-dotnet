using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Sending and receiving capabilities for a domain.
/// </summary>
public class DomainCapabilities
{
    /// <summary>
    /// Whether sending is enabled for this domain. Example values: <c>enabled</c>, <c>disabled</c>.
    /// </summary>
    [JsonPropertyName( "sending" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Sending { get; set; }

    /// <summary>
    /// Whether receiving is enabled for this domain. Example values: <c>enabled</c>, <c>disabled</c>.
    /// </summary>
    [JsonPropertyName( "receiving" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Receiving { get; set; }
}
