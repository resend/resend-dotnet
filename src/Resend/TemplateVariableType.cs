using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Type of template variable.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<TemplateVariableType> ) )]
public enum TemplateVariableType
{
    /// <summary>
    /// String
    /// </summary>
    [JsonStringValue( "string" )]
    String = 1,

    /// <summary>
    /// Number
    /// </summary>
    [JsonStringValue( "number" )]
    Number,

    /// <summary>
    /// Boolean
    /// </summary>
    [JsonStringValue( "boolean" )]
    Boolean,

    /// <summary>
    /// Instance of an object.
    /// </summary>
    [JsonStringValue( "object" )]
    Object,

    /// <summary>
    /// List of values.
    /// </summary>
    [JsonStringValue( "list" )]
    List,
}