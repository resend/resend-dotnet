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

            case WebhookEventType.ContactTopicsUpdated:
                return WebhookEventTypeCategory.ContactTopics;

            case WebhookEventType.SuppressionAdded:
            case WebhookEventType.SuppressionRemoved:
                return WebhookEventTypeCategory.Suppression;

            case WebhookEventType.TopicCreated:
            case WebhookEventType.TopicUpdated:
            case WebhookEventType.TopicDeleted:
                return WebhookEventTypeCategory.Topic;

            case WebhookEventType.InboxCreated:
            case WebhookEventType.InboxUpdated:
                return WebhookEventTypeCategory.Inbox;

            case WebhookEventType.InboxDeleted:
                return WebhookEventTypeCategory.InboxDeleted;

            case WebhookEventType.InboxThreadCreated:
            case WebhookEventType.InboxEmailReceived:
            case WebhookEventType.InboxEmailSent:
                return WebhookEventTypeCategory.InboxThreadEmail;

            case WebhookEventType.InboxThreadFolderUpdated:
                return WebhookEventTypeCategory.InboxThreadFolder;

            case WebhookEventType.InboxThreadAssigned:
                return WebhookEventTypeCategory.InboxThreadAssigned;

            case WebhookEventType.InboxThreadUnassigned:
                return WebhookEventTypeCategory.InboxThreadUnassigned;

            case WebhookEventType.InboxThreadLabelsUpdated:
                return WebhookEventTypeCategory.InboxThreadLabels;

            case WebhookEventType.InboxDraftCreated:
            case WebhookEventType.InboxDraftUpdated:
            case WebhookEventType.InboxDraftDeleted:
                return WebhookEventTypeCategory.InboxDraft;

            case WebhookEventType.InboxDraftSent:
                return WebhookEventTypeCategory.InboxDraftSent;

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