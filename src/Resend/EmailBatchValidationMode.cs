using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Determines behavior when sending batch emails.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<EmailBatchValidationMode> ) )]
public enum EmailBatchValidationMode
{
    /// <summary>
    /// Sends the batch only if all emails in the request are valid.
    /// </summary>
    Strict = 1,

    /// <summary>
    /// Processes all emails, allowing for partial success and returning
    /// validation errors if present.
    /// </summary>
    Permissive,
}