using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Writes only the assigned properties of <see cref="InboxAgentUpdateData"/>, because the
/// API treats an explicit null as a request to clear the field.
/// </summary>
public class InboxAgentUpdateDataConverter : JsonConverter<InboxAgentUpdateData>
{
    /// <inheritdoc />
    public override InboxAgentUpdateData Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        if ( reader.TokenType != JsonTokenType.StartObject )
            throw new JsonException( $"IA001: Expected StartObject, instead got '{reader.TokenType}'." );

        var value = new InboxAgentUpdateData();

        while ( reader.Read() == true )
        {
            if ( reader.TokenType == JsonTokenType.EndObject )
                return value;

            var name = reader.GetString();
            reader.Read();

            switch ( name )
            {
                case "instructions":
                    value.Instructions = JsonSerializer.Deserialize<string?>( ref reader, options );
                    break;

                case "tone":
                    value.Tone = JsonSerializer.Deserialize<InboxAgentTone?>( ref reader, options );
                    break;

                case "actions":
                    value.Actions = JsonSerializer.Deserialize<List<InboxAgentAction>?>( ref reader, options );
                    break;

                default:
                    reader.Skip();
                    break;
            }
        }

        throw new JsonException( "IA002: No more content, expected EndObject as terminator" );
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, InboxAgentUpdateData value, JsonSerializerOptions options )
    {
        writer.WriteStartObject();

        if ( value.IsAssigned( "instructions" ) == true )
        {
            writer.WritePropertyName( "instructions" );
            JsonSerializer.Serialize( writer, value.Instructions, options );
        }

        if ( value.IsAssigned( "tone" ) == true )
        {
            writer.WritePropertyName( "tone" );
            JsonSerializer.Serialize( writer, value.Tone, options );
        }

        if ( value.Actions != null )
        {
            writer.WritePropertyName( "actions" );
            JsonSerializer.Serialize( writer, value.Actions, options );
        }

        writer.WriteEndObject();
    }
}
