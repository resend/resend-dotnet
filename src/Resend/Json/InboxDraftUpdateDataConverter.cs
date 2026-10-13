using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Writes only the assigned properties of <see cref="InboxDraftUpdateData"/>, because the
/// API treats an explicit null as a request to clear the field.
/// </summary>
public class InboxDraftUpdateDataConverter : JsonConverter<InboxDraftUpdateData>
{
    /// <inheritdoc />
    public override InboxDraftUpdateData Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        if ( reader.TokenType != JsonTokenType.StartObject )
            throw new JsonException( $"ID001: Expected StartObject, instead got '{reader.TokenType}'." );

        var value = new InboxDraftUpdateData();

        while ( reader.Read() == true )
        {
            if ( reader.TokenType == JsonTokenType.EndObject )
                return value;

            var name = reader.GetString();
            reader.Read();

            switch ( name )
            {
                case "to":
                    value.To = JsonSerializer.Deserialize<EmailAddressList?>( ref reader, options );
                    break;

                case "cc":
                    value.Cc = JsonSerializer.Deserialize<EmailAddressList?>( ref reader, options );
                    break;

                case "bcc":
                    value.Bcc = JsonSerializer.Deserialize<EmailAddressList?>( ref reader, options );
                    break;

                case "subject":
                    value.Subject = JsonSerializer.Deserialize<string?>( ref reader, options );
                    break;

                case "html":
                    value.HtmlBody = JsonSerializer.Deserialize<string?>( ref reader, options );
                    break;

                case "text":
                    value.TextBody = JsonSerializer.Deserialize<string?>( ref reader, options );
                    break;

                case "attachments":
                    value.Attachments = JsonSerializer.Deserialize<List<EmailAttachment>?>( ref reader, options );
                    break;

                default:
                    reader.Skip();
                    break;
            }
        }

        throw new JsonException( "ID002: No more content, expected EndObject as terminator" );
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, InboxDraftUpdateData value, JsonSerializerOptions options )
    {
        writer.WriteStartObject();

        WriteAssigned( writer, value, "to", value.To, options );
        WriteAssigned( writer, value, "cc", value.Cc, options );
        WriteAssigned( writer, value, "bcc", value.Bcc, options );
        WriteAssigned( writer, value, "subject", value.Subject, options );
        WriteAssigned( writer, value, "html", value.HtmlBody, options );
        WriteAssigned( writer, value, "text", value.TextBody, options );

        if ( value.Attachments != null )
        {
            writer.WritePropertyName( "attachments" );
            JsonSerializer.Serialize( writer, value.Attachments, options );
        }

        writer.WriteEndObject();
    }


    /// <summary />
    private static void WriteAssigned<T>( Utf8JsonWriter writer, InboxDraftUpdateData value, string propertyName, T propertyValue, JsonSerializerOptions options )
    {
        if ( value.IsAssigned( propertyName ) == false )
            return;

        writer.WritePropertyName( propertyName );
        JsonSerializer.Serialize( writer, propertyValue, options );
    }
}
