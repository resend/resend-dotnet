using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Scope of a domain verification.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<DomainVerificationScope> ) )]
public enum DomainVerificationScope
{
    /// <summary>
    /// Verify only the Forge record. The other records keep their current status.
    /// </summary>
    [JsonStringValue( "forge" )]
    Forge,
}
