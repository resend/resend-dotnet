using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class JsonStringEnumValueConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    /// <inheritdoc />
    public override T Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        if ( reader.TokenType != JsonTokenType.String )
            throw new JsonException( $"SE001: Expected String, instead {reader.TokenType}" );

        var json = reader.GetString()!;


        /*
         * 
         */
        if ( JsonStringEnumValue<T>.Reverse.TryGetValue( json, out var rev ) == false )
            throw new JsonException( $"SE002: Invalid value: '{json}'" );

        return rev;
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, T value, JsonSerializerOptions options )
    {
        if ( JsonStringEnumValue<T>.Forward.TryGetValue( value, out var str ) == false )
            throw new JsonException( $"SE003: Invalid '{typeof( T ).Name}' value: '{value}'" );

        writer.WriteStringValue( str );
    }
}
