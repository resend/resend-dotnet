using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// What made the change that an inbox webhook event reports.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxEventSource> ) )]
public enum InboxEventSource
{
    /// <summary>
    /// A call to the API.
    /// </summary>
    [JsonStringValue( "api" )]
    Api,

    /// <summary>
    /// An action in the dashboard.
    /// </summary>
    [JsonStringValue( "dashboard" )]
    Dashboard,

    /// <summary>
    /// The agent of the inbox.
    /// </summary>
    [JsonStringValue( "agent" )]
    Agent,

    /// <summary>
    /// Resend itself, such as the email pipeline.
    /// </summary>
    [JsonStringValue( "system" )]
    System,
}
