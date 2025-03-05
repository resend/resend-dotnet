using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class EmailAddressConverter : JsonConverter<EmailAddress>
{
    /// <inheritdoc />
    public override EmailAddress? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        if ( reader.TokenType != JsonTokenType.String )
            throw new JsonException( $"EA001: Expected String, instead found '{reader.TokenType}'" );

        var addr = reader.GetString()!;

        return EmailAddress.Parse( addr );
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, EmailAddress value, JsonSerializerOptions options )
    {
        writer.WriteStringValue( value.ToString() );
    }
}
