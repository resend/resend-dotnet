using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary />
public class BroadcastScheduleRequest
{
    /// <summary />
    [JsonPropertyName( "scheduled_at" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTimeOrHuman? MomentSchedule { get; set; }
}