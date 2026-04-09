using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Response from stopping an automation run (automation is set to <c>disabled</c>).
/// </summary>
public class AutomationStopResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Automation identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Updated status (for example <c>disabled</c>).
    /// </summary>
    [JsonPropertyName( "status" )]
    public string Status { get; set; } = default!;
}
