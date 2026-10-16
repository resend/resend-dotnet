using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Type of an inbox draft.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-draft" />
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxDraftType> ) )]
public enum InboxDraftType
{
    /// <summary>
    /// The draft starts a new thread.
    /// </summary>
    [JsonStringValue( "standalone" )]
    Standalone,

    /// <summary>
    /// The draft replies to an email in an existing thread.
    /// </summary>
    [JsonStringValue( "reply" )]
    Reply,
}
