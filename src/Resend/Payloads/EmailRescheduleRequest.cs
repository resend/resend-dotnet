using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary />
public class EmailRescheduleRequest
{
    /// <summary />
    [JsonPropertyName( "scheduled_at" )]
    public DateTimeOrHuman MomentSchedule { get; set; }
}