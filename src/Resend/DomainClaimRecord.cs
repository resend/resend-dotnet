using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// TXT record that must be added to DNS to verify a domain claim.
/// </summary>
public class DomainClaimRecord
{
    /// <summary>
    /// Type of DNS record to be added.
    /// </summary>
    /// <remarks>
    /// Example value: TXT.
    /// </remarks>
    [JsonPropertyName( "type" )]
    public string RecordType { get; set; } = default!;

    /// <summary>
    /// Name of the DNS record required for verification.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Value of the DNS record required for verification.
    /// </summary>
    [JsonPropertyName( "value" )]
    public string Value { get; set; } = default!;

    /// <summary>
    /// Time to Live, in seconds -- or Auto.
    /// </summary>
    [JsonPropertyName( "ttl" )]
    public string TimeToLive { get; set; } = default!;
}
