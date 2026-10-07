using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Resend.Webhooks;

/// <summary />
public class WebhookEventConverter : JsonConverter<WebhookEvent>
{
    private readonly JsonStringEnumValueConverter<WebhookEventType> _wet;
    private readonly JsonUtcDateTimeConverter _utc;

    /// <summary />
    public WebhookEventConverter()
    {
        _wet = new JsonStringEnumValueConverter<WebhookEventType>();
        _utc = new JsonUtcDateTimeConverter();
    }


    /// <inheritdoc />
    public override WebhookEvent? Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
    {
        var value = new WebhookEvent();


        /*
         * 
         */
        if ( reader.TokenType != JsonTokenType.StartObject )
            throw new JsonException( "Expected StartObject" );


        /*
         * 
         */
        bool hasType = false;
        bool hasCreatedAt = false;
        bool hasData = false;

        JsonElement rawData = default;

        // Read the 3 webhook payload properties
        for ( int i = 0; i < 3; i++ )
        {
            reader.Read();

            if ( reader.TokenType != JsonTokenType.PropertyName )
                throw new JsonException( "Expected PropertyName" );

            string? propertyName = reader.GetString();

            if ( propertyName == "type" )
            {
                if ( hasType )
                    throw new JsonException( "Duplicate 'type' property" );

                hasType = true;

                reader.Read();
                value.EventType = _wet.Read( ref reader, typeof( WebhookEventType ), options );
            }
            else if ( propertyName == "created_at" )
            {
                if ( hasCreatedAt )
                    throw new JsonException( "Duplicate 'created_at' property" );

                hasCreatedAt = true;

                reader.Read();
                value.MomentCreated = _utc.Read( ref reader, typeof( DateTime ), options );
            }
            else if ( propertyName == "data" )
            {
                if ( hasData )
                    throw new JsonException( "Duplicate 'data' property" );

                hasData = true;

                reader.Read();
                rawData = JsonElement.ParseValue( ref reader );
            }
            else
            {
                throw new JsonException( "Invalid property name" );
            }
        }

        if ( !hasType || !hasCreatedAt || !hasData )
            throw new JsonException( "Missing required webhook properties" );


        /*
         * 
         */
        var category = value.EventType.Category();


        /*
         * 
         */
        if ( category == WebhookEventTypeCategory.Email )
        {
            var data = rawData.Deserialize( (JsonTypeInfo<EmailEventData>) options.GetTypeInfo( typeof( EmailEventData ) ) );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.Contact )
        {
            var data = rawData.Deserialize( (JsonTypeInfo<ContactEventData>) options.GetTypeInfo( typeof( ContactEventData ) ) );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.Domain )
        {
            var data = rawData.Deserialize( (JsonTypeInfo<DomainEventData>) options.GetTypeInfo( typeof( DomainEventData ) ) );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.Suppression )
        {
            var data = rawData.Deserialize( (JsonTypeInfo<SuppressionEventData>) options.GetTypeInfo( typeof( SuppressionEventData ) ) );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.ContactTopics )
        {
            var data = rawData.Deserialize( (JsonTypeInfo<ContactTopicsEventData>) options.GetTypeInfo( typeof( ContactTopicsEventData ) ) );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.Topic )
        {
            var data = rawData.Deserialize( (JsonTypeInfo<TopicEventData>) options.GetTypeInfo( typeof( TopicEventData ) ) );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else
        {
            throw new NotSupportedException( $"Unexpected '{value.EventType}' event type" );
        }


        /*
         *
         */
        reader.Read();

        if ( reader.TokenType != JsonTokenType.EndObject )
            throw new JsonException( "Expected EndObject" );

        return value;
    }


    /// <inheritdoc />
    public override void Write( Utf8JsonWriter writer, WebhookEvent value, JsonSerializerOptions options )
    {
        /*
         * 
         */
        writer.WriteStartObject();

        writer.WritePropertyName( "type" );
        _wet.Write( writer, value.EventType, options );

        writer.WritePropertyName( "created_at" );
        _utc.Write( writer, value.MomentCreated, options );


        /*
         * 
         */
        writer.WritePropertyName( "data" );

        if ( value.EventType.Category() == WebhookEventTypeCategory.Email )
        {
            JsonSerializer.Serialize( writer, value.DataAs<EmailEventData>(), (JsonTypeInfo<EmailEventData>) options.GetTypeInfo( typeof( EmailEventData ) ) );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.Contact )
        {
            JsonSerializer.Serialize( writer, value.DataAs<ContactEventData>(), (JsonTypeInfo<ContactEventData>) options.GetTypeInfo( typeof( ContactEventData ) ) );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.Domain )
        {
            JsonSerializer.Serialize( writer, value.DataAs<DomainEventData>(), (JsonTypeInfo<DomainEventData>) options.GetTypeInfo( typeof( DomainEventData ) ) );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.Suppression )
        {
            JsonSerializer.Serialize( writer, value.DataAs<SuppressionEventData>(), (JsonTypeInfo<SuppressionEventData>) options.GetTypeInfo( typeof( SuppressionEventData ) ) );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.ContactTopics )
        {
            JsonSerializer.Serialize( writer, value.DataAs<ContactTopicsEventData>(), (JsonTypeInfo<ContactTopicsEventData>) options.GetTypeInfo( typeof( ContactTopicsEventData ) ) );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.Topic )
        {
            JsonSerializer.Serialize( writer, value.DataAs<TopicEventData>(), (JsonTypeInfo<TopicEventData>) options.GetTypeInfo( typeof( TopicEventData ) ) );
        }
        else
        {
            throw new NotSupportedException( $"Unexpected '{value.EventType}' event type" );
        }


        /*
         * 
         */
        writer.WriteEndObject();
    }
}
