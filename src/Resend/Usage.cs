using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Account-level usage and quota data.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/usage/retrieve-usage"/>
public class Usage
{
    /// <summary />
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Email sending and receiving usage.
    /// </summary>
    [JsonPropertyName( "emails" )]
    public UsageEmails Emails { get; set; } = default!;

    /// <summary>
    /// Contact usage.
    /// </summary>
    [JsonPropertyName( "contacts" )]
    public UsageContacts Contacts { get; set; } = default!;

    /// <summary>
    /// Segment usage.
    /// </summary>
    [JsonPropertyName( "segments" )]
    public UsageSegments Segments { get; set; } = default!;

    /// <summary>
    /// Broadcast usage.
    /// </summary>
    [JsonPropertyName( "broadcasts" )]
    public UsageBroadcasts Broadcasts { get; set; } = default!;

    /// <summary>
    /// AI credits usage.
    /// </summary>
    [JsonPropertyName( "ai_credits" )]
    public UsageAiCredits AiCredits { get; set; } = default!;

    /// <summary>
    /// Automation run usage.
    /// </summary>
    [JsonPropertyName( "automation_runs" )]
    public UsageAutomationRuns AutomationRuns { get; set; } = default!;

    /// <summary>
    /// Verified domain usage.
    /// </summary>
    [JsonPropertyName( "domains" )]
    public UsageDomains Domains { get; set; } = default!;

    /// <summary>
    /// API rate limit that applies to this account.
    /// </summary>
    [JsonPropertyName( "rate_limit" )]
    public UsageRateLimit RateLimit { get; set; } = default!;
}
