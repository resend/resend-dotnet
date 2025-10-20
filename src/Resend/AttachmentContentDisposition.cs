using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
[JsonConverter( typeof( JsonStringEnumValueConverter<AttachmentContentDisposition> ) )]
public enum AttachmentContentDisposition
{
    /// <summary>
    /// Content is attached to the email.
    /// </summary>
    [JsonStringValue( "attachment" )]
    Attachment = 1,

    /// <summary>
    /// Content is inline to the email.
    /// </summary>
    [JsonStringValue( "inline" )]
    Inline,
}