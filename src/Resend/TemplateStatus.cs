using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Type of template variable.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<TemplateStatus> ) )]
public enum TemplateStatus
{
    /// <summary>
    /// String
    /// </summary>
    [JsonStringValue( "draft" )]
    Draft = 1,

    /// <summary>
    /// Number
    /// </summary>
    [JsonStringValue( "published" )]
    Published,
}