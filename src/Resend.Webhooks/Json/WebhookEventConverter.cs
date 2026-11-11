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
        else if ( category == WebhookEventTypeCategory.Inbox )
        {
            var data = rawData.Deserialize<InboxEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxDeleted )
        {
            var data = rawData.Deserialize<InboxDeletedEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxThreadEmail )
        {
            var data = rawData.Deserialize<InboxThreadEmailEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxThreadFolder )
        {
            var data = rawData.Deserialize<InboxThreadFolderUpdatedEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxThreadAssigned )
        {
            var data = rawData.Deserialize<InboxThreadAssignedEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxThreadUnassigned )
        {
            var data = rawData.Deserialize<InboxThreadUnassignedEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxThreadLabels )
        {
            var data = rawData.Deserialize<InboxThreadLabelsUpdatedEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxDraft )
        {
            var data = rawData.Deserialize<InboxDraftEventData>( options );

            if ( data == null )
                throw new JsonException( "Expected non-null data" );

            value.Data = data;
        }
        else if ( category == WebhookEventTypeCategory.InboxDraftSent )
        {
            var data = rawData.Deserialize<InboxDraftSentEventData>( options );

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
        else if ( value.EventType.Category() == WebhookEventTypeCategory.Inbox )
        {
            var t7 = typeof( InboxEventData );
            var o7 = (JsonConverter<InboxEventData>) options.GetConverter( t7 );

            o7.Write( writer, value.DataAs<InboxEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxDeleted )
        {
            var t8 = typeof( InboxDeletedEventData );
            var o8 = (JsonConverter<InboxDeletedEventData>) options.GetConverter( t8 );

            o8.Write( writer, value.DataAs<InboxDeletedEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxThreadEmail )
        {
            var t9 = typeof( InboxThreadEmailEventData );
            var o9 = (JsonConverter<InboxThreadEmailEventData>) options.GetConverter( t9 );

            o9.Write( writer, value.DataAs<InboxThreadEmailEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxThreadFolder )
        {
            var t10 = typeof( InboxThreadFolderUpdatedEventData );
            var o10 = (JsonConverter<InboxThreadFolderUpdatedEventData>) options.GetConverter( t10 );

            o10.Write( writer, value.DataAs<InboxThreadFolderUpdatedEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxThreadAssigned )
        {
            var t11 = typeof( InboxThreadAssignedEventData );
            var o11 = (JsonConverter<InboxThreadAssignedEventData>) options.GetConverter( t11 );

            o11.Write( writer, value.DataAs<InboxThreadAssignedEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxThreadUnassigned )
        {
            var t12 = typeof( InboxThreadUnassignedEventData );
            var o12 = (JsonConverter<InboxThreadUnassignedEventData>) options.GetConverter( t12 );

            o12.Write( writer, value.DataAs<InboxThreadUnassignedEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxThreadLabels )
        {
            var t13 = typeof( InboxThreadLabelsUpdatedEventData );
            var o13 = (JsonConverter<InboxThreadLabelsUpdatedEventData>) options.GetConverter( t13 );

            o13.Write( writer, value.DataAs<InboxThreadLabelsUpdatedEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxDraft )
        {
            var t14 = typeof( InboxDraftEventData );
            var o14 = (JsonConverter<InboxDraftEventData>) options.GetConverter( t14 );

            o14.Write( writer, value.DataAs<InboxDraftEventData>(), options );
        }
        else if ( value.EventType.Category() == WebhookEventTypeCategory.InboxDraftSent )
        {
            var t15 = typeof( InboxDraftSentEventData );
            var o15 = (JsonConverter<InboxDraftSentEventData>) options.GetConverter( t15 );

            o15.Write( writer, value.DataAs<InboxDraftSentEventData>(), options );
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
