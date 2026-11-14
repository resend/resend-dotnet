using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object for verifying a domain.
/// </summary>
public class DomainVerifyData
{
    /// <summary>
    /// Limit the check to specific records.
    /// </summary>
    /// <remarks>
    /// Pass <see cref="DomainVerificationScope.Forge"/> to verify only the Forge
    /// record and leave the other records untouched. Requires Forge to be enabled
    /// on the domain. When omitted, all records are checked.
    /// </remarks>
    [JsonPropertyName( "scope" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<DomainVerificationScope>? Scope { get; set; }
}


/// <summary>
/// Scope of a domain verification.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<DomainVerificationScope> ) )]
public enum DomainVerificationScope
{
    /// <summary>
    /// Verify only the Forge return path record.
    /// </summary>
    [JsonStringValue( "forge" )]
    Forge,
}
