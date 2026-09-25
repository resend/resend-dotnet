using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email sending and receiving usage, broken down by reporting window.
/// </summary>
public class UsageEmails
{
    /// <summary>
    /// Usage for the current day.
    /// </summary>
    [JsonPropertyName( "daily" )]
    public UsageEmailDaily Daily { get; set; } = default!;

    /// <summary>
    /// Usage for the current billing month.
    /// </summary>
    [JsonPropertyName( "monthly" )]
    public UsageEmailMonthly Monthly { get; set; } = default!;
}
