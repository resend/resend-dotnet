using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A claim on a domain that is already verified by another team.
/// </summary>
/// <see href="https://resend.com/docs/dashboard/domains/claim" />
public class DomainClaim
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Object { get; set; }

    /// <summary>
    /// Claim identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Domain name being claimed.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Status of the claim.
    /// </summary>
    [JsonPropertyName( "status" )]
    public DomainClaimStatus Status { get; set; }

    /// <summary>
    /// Identifier of the placeholder domain created for this claim.
    /// </summary>
    [JsonPropertyName( "domain_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? DomainId { get; set; }

    /// <summary>
    /// Region from which the emails for this domain are delivered.
    /// </summary>
    [JsonPropertyName( "region" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DeliveryRegion? Region { get; set; }

    /// <summary>
    /// TXT record that must be added to DNS to verify the claim.
    /// </summary>
    [JsonPropertyName( "record" )]
    public DomainClaimRecord Record { get; set; } = default!;

    /// <summary>
    /// Reason the claim is blocked, if it is blocked by an ownership-safety check.
    /// </summary>
    /// <remarks>
    /// Free-text on the API; observed values include <c>grace_period</c> and
    /// <c>recent_owner_activity</c>. Not modelled as an enum so that unknown values
    /// do not break deserialization.
    /// </remarks>
    [JsonPropertyName( "blocked_reason" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? BlockedReason { get; set; }

    /// <summary>
    /// Reason the claim failed, if it has failed.
    /// </summary>
    [JsonPropertyName( "failure_reason" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? FailureReason { get; set; }

    /// <summary>
    /// Moment when the claim was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment when the claim expires.
    /// </summary>
    [JsonPropertyName( "expires_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentExpires { get; set; }
}
