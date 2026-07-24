using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Status of a domain claim.
/// </summary>
/// <see href="https://resend.com/docs/dashboard/domains/claim" />
[JsonConverter( typeof( JsonStringEnumValueConverter<DomainClaimStatus> ) )]
public enum DomainClaimStatus
{
    /// <summary>
    /// Claim has been created and is awaiting DNS verification.
    /// </summary>
    [JsonStringValue( "pending" )]
    Pending,

    /// <summary>
    /// Claim's TXT record has been verified.
    /// </summary>
    [JsonStringValue( "verified" )]
    Verified,

    /// <summary>
    /// Claim has completed and the domain has been transferred.
    /// </summary>
    [JsonStringValue( "completed" )]
    Completed,

    /// <summary>
    /// Claim is temporarily blocked by an ownership-safety check.
    /// </summary>
    [JsonStringValue( "blocked" )]
    Blocked,

    /// <summary>
    /// Claim has expired before being verified.
    /// </summary>
    [JsonStringValue( "expired" )]
    Expired,

    /// <summary>
    /// Claim has been superseded by a newer claim for the same domain.
    /// </summary>
    [JsonStringValue( "superseded" )]
    Superseded,

    /// <summary>
    /// Claim has been canceled.
    /// </summary>
    [JsonStringValue( "canceled" )]
    Canceled,

    /// <summary>
    /// Claim has failed.
    /// </summary>
    [JsonStringValue( "failed" )]
    Failed,
}
