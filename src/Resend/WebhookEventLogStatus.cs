using System.Text.Json.Serialization;

namespace Resend;

[JsonConverter( typeof( JsonStringEnumValueConverter<WebhookEventLogStatus> ) )]
public enum WebhookEventLogStatus
{
    [JsonStringValue( "pending" )]
    Pending = 1,

    [JsonStringValue( "attempting" )]
    Attempting,

    [JsonStringValue( "success" )]
    Success,

    [JsonStringValue( "failed" )]
    Failed,
}
