using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Origin of a suppression.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/suppressions/list-suppressions" />
[JsonConverter( typeof( JsonStringEnumValueConverter<SuppressionOrigin> ) )]
public enum SuppressionOrigin
{
    /// <summary>
    /// Suppression was created because an email to the address bounced.
    /// </summary>
    [JsonStringValue( "bounce" )]
    Bounce,

    /// <summary>
    /// Suppression was created because the recipient marked an email as spam.
    /// </summary>
    [JsonStringValue( "complaint" )]
    Complaint,

    /// <summary>
    /// Suppression was created manually.
    /// </summary>
    [JsonStringValue( "manual" )]
    Manual,
}
