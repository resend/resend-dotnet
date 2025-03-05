using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class EmailAddressListConverter : JsonConverter<EmailAddressList>
{
    /// <inheritdoc />
    public override EmailAddressList Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        var list = new EmailAddressList();


        /*
         * If it's a singular string, then it's just one single email address.
         */
        if ( reader.TokenType == JsonTokenType.String )
        {
            var s = reader.GetString()!;
            list.Add( s );

            return list;
        }

        if ( reader.TokenType != JsonTokenType.StartArray )
            throw new JsonException( $"EL001: Expected String | StartArray, instead got '{reader.TokenType}'." );


        /*
         * 
         */
        while ( reader.Read() == true )
        {
            if ( reader.TokenType == JsonTokenType.EndArray )
                return list;

            if ( reader.TokenType != JsonTokenType.String )
                throw new JsonException( $"EL002: Expected String, instead got '{reader.TokenType}'." );

            var s = reader.GetString()!;
            list.Add( s );
        }

        throw new JsonException( $"EL003: No more content, expected EndArray as terminator" );
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, EmailAddressList value, JsonSerializerOptions options )
    {
        /*
         * 
         */
        writer.WriteStartArray();

        foreach ( var s in value )
            writer.WriteStringValue( s.ToString() );

        writer.WriteEndArray();
    }
}
