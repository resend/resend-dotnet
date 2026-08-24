using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Broadcast recipient bounce classifications.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<BroadcastRecipientBounceType> ) )]
public enum BroadcastRecipientBounceType
{
    /// <summary>
    /// The bounce is permanent; the address is not expected to become deliverable.
    /// </summary>
    [JsonStringValue( "permanent" )]
    Permanent,

    /// <summary>
    /// The bounce is transient; the address may become deliverable again.
    /// </summary>
    [JsonStringValue( "transient" )]
    Transient,

    /// <summary>
    /// The bounce could not be classified.
    /// </summary>
    [JsonStringValue( "undetermined" )]
    Undetermined,
}
