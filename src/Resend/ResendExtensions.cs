namespace Resend;

/// <summary />
public static class ResendExtensions
{
    /// <summary>
    /// Returns the event category for a given event type.
    /// </summary>
    public static WebhookEventTypeCategory Category( this WebhookEventType @event )
    {
        switch ( @event )
        {
            case WebhookEventType.DomainCreated:
            case WebhookEventType.DomainUpdated:
            case WebhookEventType.DomainDeleted:
                return WebhookEventTypeCategory.Domain;

            case WebhookEventType.ContactCreated:
            case WebhookEventType.ContactUpdated:
            case WebhookEventType.ContactDeleted:
                return WebhookEventTypeCategory.Contact;

            case WebhookEventType.EmailBounced:
            case WebhookEventType.EmailClicked:
            case WebhookEventType.EmailComplained:
            case WebhookEventType.EmailDelivered:
            case WebhookEventType.EmailDeliveryDelay:
            case WebhookEventType.EmailFailed:
            case WebhookEventType.EmailOpened:
            case WebhookEventType.EmailReceived:
            case WebhookEventType.EmailScheduled:
            case WebhookEventType.EmailSent:
            case WebhookEventType.EmailSuppressed:
                return WebhookEventTypeCategory.Email;

            default:
                throw new NotSupportedException();
        }
    }
}