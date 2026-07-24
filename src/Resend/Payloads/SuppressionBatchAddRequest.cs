using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary>
/// Request object to add up to 100 suppressions at once.
/// </summary>
public class SuppressionBatchAddRequest
{
    /// <summary>
    /// Email addresses to suppress.
    /// </summary>
    [JsonPropertyName( "emails" )]
    public List<string> Emails { get; set; } = default!;
}
