using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Whether the inbox received an email or sent it.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-thread-email" />
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxEmailDirection> ) )]
public enum InboxEmailDirection
{
    /// <summary>
    /// The inbox received the email.
    /// </summary>
    /// <remarks>
    /// A message the inbox sent to itself is inbound, so a reply to it goes to its sender.
    /// </remarks>
    [JsonStringValue( "inbound" )]
    Inbound,

    /// <summary>
    /// The inbox sent the email.
    /// </summary>
    [JsonStringValue( "outbound" )]
    Outbound,
}
