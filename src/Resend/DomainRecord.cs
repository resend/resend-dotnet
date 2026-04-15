using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class DomainRecord
{
    /// <summary />
    /// <remarks>
    /// Example values: SPF, DKIM, Receiving, Tracking, TrackingCAA.
    /// </remarks>
    [JsonPropertyName( "record" )]
    public string Record { get; set; } = default!;

    /// <summary>
    /// Name of the DNS record required for verification.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Value of the DNS record required for verification.
    /// </summary>
    [JsonPropertyName( "value" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Value { get; set; }

    /// <summary>
    /// Type of DNS record to be added.
    /// </summary>
    /// <remarks>
    /// Example values: TXT, MX.
    /// </remarks>
    [JsonPropertyName( "type" )]
    public string RecordType { get; set; } = default!;

    /// <summary>
    /// Time to Live, in seconds -- or Auto.
    /// </summary>
    [JsonPropertyName( "ttl" )]
    public string TimeToLive { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "priority" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public int? Priority { get; set; } = default!;

    /// <summary>
    /// Validation status of individual DNS record.
    /// </summary>
    [JsonPropertyName( "status" )]
    public ValidationStatus Status { get; set; }
}
