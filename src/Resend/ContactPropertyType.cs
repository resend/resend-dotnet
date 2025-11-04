using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Contact property data-type.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<ContactPropertyType> ) )]
public enum ContactPropertyType
{
    /// <summary>
    /// String.
    /// </summary>
    [JsonStringValue( "string" )]
    String = 1,

    /// <summary>
    /// Number.
    /// </summary>
    [JsonStringValue( "number" )]
    Number,
}