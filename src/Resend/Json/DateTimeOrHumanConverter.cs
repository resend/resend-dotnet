using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Resend;

/// <summary />
public class DateTimeOrHumanConverter : JsonConverter<DateTimeOrHuman>
{
    private static Regex _ymd = new Regex( @"^\d{4}-\d{2}-\d{2}" );


    /// <summary />
    public override DateTimeOrHuman Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        if ( reader.TokenType != JsonTokenType.String )
            throw new JsonException( $"Expected String when converting DateTimeOrHuman, found {reader.TokenType}" );


        /*
         * 
         */
        var str = reader.GetString()!;

        if ( str.Length == 0 )
            throw new JsonException( $"Empty value is not expected" );


        /*
         * If the string doesn't start with yyyy-MM-dd, then assume it's a human
         * string and return that instead.
         */
        if ( _ymd.IsMatch( str ) == false )
            return str;


        /*
         * The API examples have the following values:
         * 2023-04-08T00:11:13.110779+00:00
         * 2023-04-26T20:21:26.347412+00:00
         */
        DateTime value;

        try
        {
            // TODO: Consider exact parsing?

            value = DateTime.Parse( str );
        }
        catch ( FormatException ex )
        {
            throw new JsonException( $"Value '{str}' is not valid dateTime", ex );
        }


        /*
         * Ensure UTC
         */
        if ( value.Kind == DateTimeKind.Utc )
            return value;

        return value.ToUniversalTime();
    }


    /// <summary />
    public override void Write( Utf8JsonWriter writer, DateTimeOrHuman value, JsonSerializerOptions options )
    {
        if ( value.IsMoment == true )
        {
            writer.WriteStringValue( value.Moment.ToUniversalTime().ToString( "yyyy-MM-ddTHH:mm:ssZ" ) );
        }
        else
        {
            writer.WriteStringValue( value.Human );
        }
    }
}