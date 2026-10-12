using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
/// <remarks>
/// See https://www.resend.com/docs/dashboard/webhooks/event-types
/// </remarks>
[JsonConverter( typeof( JsonStringEnumValueConverter<WebhookEventType> ) )]
public enum WebhookEventType
{
    /// <summary>
    /// The API request was successful and Resend will attempt to deliver
    /// the message to the recipient’s mail server.
    /// </summary>
    [JsonStringValue( "email.sent" )]
    EmailSent = 1,

    /// <summary>
    /// An email was received by Resend.
    /// </summary>
    /// <remarks>
    /// See https://resend.com/docs/dashboard/receiving/introduction
    /// </remarks>
    [JsonStringValue( "email.received" )]
    EmailReceived,

    /// <summary>
    /// Resend successfully delivered the email to the recipient’s mail server.
    /// </summary>
    [JsonStringValue( "email.delivered" )]
    EmailDelivered,

    /// <summary>
    /// The email couldn’t be delivered to the recipient’s mail server because
    /// a temporary issue occurred. Delivery delays can occur, for example,
    /// when the recipient’s inbox is full, or when the receiving email server
    /// experiences a transient issue.
    /// </summary>
    [JsonStringValue( "email.delivery_delayed" )]
    EmailDeliveryDelay,

    /// <summary>
    /// The email was successfully delivered to the recipient’s mail server,
    /// but the recipient marked it as spam.
    /// </summary>
    [JsonStringValue( "email.complained" )]
    EmailComplained,

    /// <summary>
    /// The recipient’s mail server permanently rejected the email.
    /// </summary>
    [JsonStringValue( "email.bounced" )]
    EmailBounced,

    /// <summary>
    /// The recipient’s clicked on an email link.
    /// </summary>
    [JsonStringValue( "email.clicked" )]
    EmailClicked,

    /// <summary>
    /// The recipient’s opened the email.
    /// </summary>
    [JsonStringValue( "email.opened" )]
    EmailOpened,

    /// <summary>
    /// Email failed to be sent due to an error.
    /// </summary>
    /// <remarks>
    /// This event is triggered when there are issues such as invalid
    /// recipients, API key problems, domain verification issues,
    /// quota limits, or other sending failures.
    /// </remarks>
    [JsonStringValue( "email.failed" )]
    EmailFailed,

    /// <summary>
    /// Email was scheduled to be sent at some point in the future.
    /// </summary>
    [JsonStringValue( "email.scheduled" )]
    EmailScheduled,

    /// <summary>
    /// The sending of this email was suppressed.
    /// </summary>
    /// <remarks>
    /// This event is triggered when an email is sent to an address
    /// on the suppression list.
    /// See https://www.resend.com/docs/knowledge-base/why-are-my-emails-landing-on-the-suppression-list
    /// </remarks>
    [JsonStringValue( "email.suppressed" )]
    EmailSuppressed,


    /// <summary>
    /// A contact was successfully created.
    /// </summary>
    [JsonStringValue( "contact.created" )]
    ContactCreated,

    /// <summary>
    /// A contact was successfully updated.
    /// </summary>
    [JsonStringValue( "contact.updated" )]
    ContactUpdated,

    /// <summary>
    /// A contact was successfully deleted.
    /// </summary>
    [JsonStringValue( "contact.deleted" )]
    ContactDeleted,


    /// <summary>
    /// A domain was successfully created.
    /// </summary>
    [JsonStringValue( "domain.created" )]
    DomainCreated,

    /// <summary>
    /// A domain was successfully updated.
    /// </summary>
    [JsonStringValue( "domain.updated" )]
    DomainUpdated,

    /// <summary>
    /// A domain was successfully deleted.
    /// </summary>
    [JsonStringValue( "domain.deleted" )]
    DomainDeleted,


    /// <summary>
    /// An email address was added to the suppression list.
    /// </summary>
    [JsonStringValue( "suppression.added" )]
    SuppressionAdded,

    /// <summary>
    /// An email address was removed from the suppression list.
    /// </summary>
    [JsonStringValue( "suppression.removed" )]
    SuppressionRemoved,


    /// <summary>
    /// A contact's topic subscriptions changed.
    /// </summary>
    [JsonStringValue( "contact.topics.updated" )]
    ContactTopicsUpdated,


    /// <summary>
    /// A topic was successfully created.
    /// </summary>
    [JsonStringValue( "topic.created" )]
    TopicCreated,

    /// <summary>
    /// A topic was successfully updated.
    /// </summary>
    [JsonStringValue( "topic.updated" )]
    TopicUpdated,

    /// <summary>
    /// A topic was successfully deleted.
    /// </summary>
    [JsonStringValue( "topic.deleted" )]
    TopicDeleted,

    /// <summary>
    /// An inbox was created.
    /// </summary>
    [JsonStringValue( "inbox.created" )]
    InboxCreated,

    /// <summary>
    /// The settings of an inbox changed.
    /// </summary>
    [JsonStringValue( "inbox.updated" )]
    InboxUpdated,

    /// <summary>
    /// An inbox was deleted. The payload carries only the inbox identifier.
    /// </summary>
    [JsonStringValue( "inbox.deleted" )]
    InboxDeleted,

    /// <summary>
    /// The first email of a thread landed in an inbox, inbound or outbound.
    /// </summary>
    [JsonStringValue( "inbox.thread.created" )]
    InboxThreadCreated,

    /// <summary>
    /// An inbound email was added to a thread.
    /// </summary>
    [JsonStringValue( "inbox.email.received" )]
    InboxEmailReceived,

    /// <summary>
    /// An outbound email was accepted for delivery and added to a thread.
    /// </summary>
    [JsonStringValue( "inbox.email.sent" )]
    InboxEmailSent,

    /// <summary>
    /// A thread moved between folders.
    /// </summary>
    [JsonStringValue( "inbox.thread.folder.updated" )]
    InboxThreadFolderUpdated,

    /// <summary>
    /// A thread was assigned or reassigned.
    /// </summary>
    [JsonStringValue( "inbox.thread.assigned" )]
    InboxThreadAssigned,

    /// <summary>
    /// A thread was unassigned.
    /// </summary>
    [JsonStringValue( "inbox.thread.unassigned" )]
    InboxThreadUnassigned,

    /// <summary>
    /// A label was applied to or removed from a thread. One event carries one label.
    /// </summary>
    [JsonStringValue( "inbox.thread.labels.updated" )]
    InboxThreadLabelsUpdated,

    /// <summary>
    /// A draft was created, standalone or reply.
    /// </summary>
    [JsonStringValue( "inbox.draft.created" )]
    InboxDraftCreated,

    /// <summary>
    /// A draft was edited.
    /// </summary>
    [JsonStringValue( "inbox.draft.updated" )]
    InboxDraftUpdated,

    /// <summary>
    /// A draft was sent.
    /// </summary>
    [JsonStringValue( "inbox.draft.sent" )]
    InboxDraftSent,

    /// <summary>
    /// A draft was discarded.
    /// </summary>
    [JsonStringValue( "inbox.draft.deleted" )]
    InboxDraftDeleted,
}
