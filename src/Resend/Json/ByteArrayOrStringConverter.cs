using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Resend;

/// <summary />
public class ByteArrayOrStringConverter : JsonConverter<ByteArrayOrString>
{
    /// <summary />
    public override ByteArrayOrString Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        if ( reader.TokenType != JsonTokenType.String )
            throw new JsonException( $"Expected String when converting ByteArrayOrString, found {reader.TokenType}" );

        var str = reader.GetString()!;

        if ( TryBase64( str, out var bytes ) == true )
            return new ByteArrayOrString( bytes );

        return new ByteArrayOrString( str );
    }


    /// <summary />
    private static bool TryBase64( string input, out byte[] bytes )
    {
        bytes = [];

        if ( string.IsNullOrWhiteSpace( input ) )
            return false;

        // Base64 strings must be a multiple of 4 in length
        if ( input.Length % 4 != 0 )
            return false;

        // Use a regex to check valid Base64 characters
        if ( Regex.IsMatch( input, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None ) == false )
            return false;

        try
        {
            bytes = Convert.FromBase64String( input );
            return true;
        }
        catch
        {
            return false;
        }
    }


    /// <summary />
    public override void Write( Utf8JsonWriter writer, ByteArrayOrString value, JsonSerializerOptions options )
    {
        if ( value.ByteArray != null )
        {
            writer.WriteBase64StringValue( value.ByteArray );
        }
        else
        {
            writer.WriteStringValue( value.String );
        }
    }
}