using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary>
/// Request object to remove up to 100 suppressions at once.
/// </summary>
/// <remarks>
/// The API accepts either <see cref="Emails"/> or <see cref="Ids"/>, never both, and
/// rejects an explicit null -- hence the unset one is omitted from the payload rather
/// than written as null.
/// </remarks>
public class SuppressionBatchRemoveRequest
{
    /// <summary>
    /// Email addresses to remove from the suppression list.
    /// </summary>
    [JsonPropertyName( "emails" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? Emails { get; set; }

    /// <summary>
    /// Suppression identifiers to remove from the suppression list.
    /// </summary>
    [JsonPropertyName( "ids" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<Guid>? Ids { get; set; }
}
