using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class Domain
{
    /// <summary>
    /// Domain identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Domain name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Validation status.
    /// </summary>
    [JsonPropertyName( "status" )]
    public DomainStatus Status { get; set; }

    /// <summary>
    /// Moment when the domain was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Region from which the emails for this domain are delivered.
    /// </summary>
    [JsonPropertyName( "region" )]
    public DeliveryRegion Region { get; set; }

    /// <summary>
    /// Whether open tracking is enabled for this domain.
    /// </summary>
    [JsonPropertyName( "open_tracking" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? OpenTracking { get; set; }

    /// <summary>
    /// Whether click tracking is enabled for this domain.
    /// </summary>
    [JsonPropertyName( "click_tracking" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? ClickTracking { get; set; }

    /// <summary>
    /// Subdomain used for click and open tracking URLs (for example, <c>links</c> for <c>links.example.com</c>).
    /// </summary>
    [JsonPropertyName( "tracking_subdomain" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TrackingSubdomain { get; set; }

    /// <summary>
    /// Whether this domain can send and receive email.
    /// </summary>
    [JsonPropertyName( "capabilities" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DomainCapabilities? Capabilities { get; set; }

    /// <summary>
    /// DNS records used for domain validation.
    /// </summary>
    /// <remarks>
    /// When the domain is programatically created through API, these DNS records
    /// need to be added so that Resend can validate ownership of the domain.
    /// </remarks>
    [JsonPropertyName( "records" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<DomainRecord>? Records { get; set; }
}
