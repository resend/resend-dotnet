using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Subscription default, for topics.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<SubscriptionType> ) )]
public enum SubscriptionType
{
    /// <summary>
    /// Recipient must explicitly opt in to receive.
    /// </summary>
    [JsonStringValue( "opt_in" )]
    OptIn = 1,

    /// <summary>
    /// Recipient must opt out (to stop receiving).
    /// </summary>
    [JsonStringValue( "opt_out" )]
    OptOut,
}