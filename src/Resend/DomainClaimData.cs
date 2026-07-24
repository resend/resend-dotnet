using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object to claim a domain that is already verified by another team.
/// </summary>
public class DomainClaimData
{
    /// <summary>
    /// Name of the domain to claim.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string DomainName { get; set; } = default!;

    /// <summary>
    /// Delivery region.
    /// </summary>
    [JsonPropertyName( "region" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DeliveryRegion? Region { get; set; }

    /// <summary>
    /// Overrides the default value of `Return-Path`.
    /// </summary>
    /// <remarks>
    /// By default, Resend will use the `send` subdomain for the `Return-Path` address.
    /// </remarks>
    [JsonPropertyName( "custom_return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? CustomReturnPath { get; set; }

    /// <summary>
    /// Track the open rate of each email.
    /// </summary>
    [JsonPropertyName( "open_tracking" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? OpenTracking { get; set; }

    /// <summary>
    /// Track clicks within the body of each HTML email.
    /// </summary>
    [JsonPropertyName( "click_tracking" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? ClickTracking { get; set; }

    /// <summary>
    /// Subdomain for click and open tracking (for example, <c>links</c> for <c>links.example.com</c>).
    /// </summary>
    [JsonPropertyName( "tracking_subdomain" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TrackingSubdomain { get; set; }
}
