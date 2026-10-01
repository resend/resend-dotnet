using System.Collections;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Converter for <c>object</c>-typed property values which does not require
/// reflection-based serialization when the runtime type has no registered
/// metadata (Native AOT / trimmed applications).
/// </summary>
/// <remarks>
/// When metadata is available (registered types, or any type when reflection
/// is enabled), serialization is delegated to <see cref="JsonSerializer"/>,
/// so output is identical to the default behavior for <c>object</c> values.
/// </remarks>
internal sealed class ObjectValueConverter : JsonConverter<object?>
{
    /// <summary>
    /// Shared instance.
    /// </summary>
    internal static ObjectValueConverter Instance { get; } = new ObjectValueConverter();


    /// <inheritdoc />
    public override object? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        return options.UnknownTypeHandling == JsonUnknownTypeHandling.JsonNode
            ? JsonNode.Parse( ref reader, new JsonNodeOptions { PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive } )
            : JsonElement.ParseValue( ref reader );
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, object? value, JsonSerializerOptions options )
    {
        if ( value == null )
        {
            writer.WriteNullValue();
            return;
        }

        var type = value.GetType();

        if ( type == typeof( object ) )
        {
            writer.WriteStartObject();
            writer.WriteEndObject();
            return;
        }

        if ( options.TryGetTypeInfo( type, out var typeInfo ) )
        {
            JsonSerializer.Serialize( writer, value, typeInfo );
            return;
        }


        /*
         * No metadata -- only reachable when reflection-based serialization
         * is disabled (Native AOT / trimming).
         */
        if ( type.IsEnum )
        {
            if ( Convert.GetTypeCode( value ) == TypeCode.UInt64 )
                writer.WriteNumberValue( Convert.ToUInt64( value ) );
            else
                writer.WriteNumberValue( Convert.ToInt64( value ) );

            return;
        }

        switch ( value )
        {
            case byte v:
                writer.WriteNumberValue( v );
                return;
            case sbyte v:
                writer.WriteNumberValue( v );
                return;
            case short v:
                writer.WriteNumberValue( v );
                return;
            case ushort v:
                writer.WriteNumberValue( v );
                return;
            case uint v:
                writer.WriteNumberValue( v );
                return;
            case ulong v:
                writer.WriteNumberValue( v );
                return;
            case char v:
                writer.WriteStringValue( v.ToString() );
                return;
        }

        if ( value is byte[] bytes )
        {
            writer.WriteBase64StringValue( bytes );
            return;
        }

        if ( value is IDictionary dictionary )
        {
            writer.WriteStartObject();

            foreach ( DictionaryEntry entry in dictionary )
            {
                var key = entry.Key as string ?? Convert.ToString( entry.Key, CultureInfo.InvariantCulture )!;
                writer.WritePropertyName( options.DictionaryKeyPolicy?.ConvertName( key ) ?? key );
                Write( writer, entry.Value, options );
            }

            writer.WriteEndObject();
            return;
        }

        if ( value is IEnumerable enumerable )
        {
            writer.WriteStartArray();

            foreach ( var item in enumerable )
                Write( writer, item, options );

            writer.WriteEndArray();
            return;
        }

        throw new NotSupportedException( $"Type '{type.FullName}' cannot be serialized in an object-typed property when reflection-based serialization is disabled (Native AOT/trimming). Use a primitive, enum, collection, dictionary, JsonElement or JsonNode." );
    }
}
