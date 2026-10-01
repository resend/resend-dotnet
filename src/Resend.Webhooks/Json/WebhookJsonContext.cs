using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary>
/// Source-generated serialization metadata for webhook events.
/// </summary>
[JsonSerializable( typeof( WebhookEvent ) )]
[JsonSerializable( typeof( EmailEventData ) )]
[JsonSerializable( typeof( ContactEventData ) )]
[JsonSerializable( typeof( DomainEventData ) )]
[JsonSerializable( typeof( SuppressionEventData ) )]
[JsonSerializable( typeof( ContactTopicsEventData ) )]
[JsonSerializable( typeof( TopicEventData ) )]
internal partial class WebhookJsonContext : JsonSerializerContext
{
}
