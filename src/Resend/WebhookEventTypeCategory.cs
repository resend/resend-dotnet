namespace Resend;

/// <summary />
public enum WebhookEventTypeCategory
{
    /// <summary>
    /// Email.
    /// </summary>
    Email = 1,

    /// <summary>
    /// Contact.
    /// </summary>
    Contact,

    /// <summary>
    /// Domain.
    /// </summary>
    Domain,

    /// <summary>
    /// Suppression.
    /// </summary>
    Suppression,

    /// <summary>
    /// Contact topic subscriptions.
    /// </summary>
    ContactTopics,

    /// <summary>
    /// Topic.
    /// </summary>
    Topic,

    /// <summary>
    /// Inbox created or updated.
    /// </summary>
    Inbox,

    /// <summary>
    /// Inbox deleted.
    /// </summary>
    InboxDeleted,

    /// <summary>
    /// Inbox thread created, or inbox email received or sent.
    /// </summary>
    InboxThreadEmail,

    /// <summary>
    /// Inbox thread moved between folders.
    /// </summary>
    InboxThreadFolder,

    /// <summary>
    /// Inbox thread assigned.
    /// </summary>
    InboxThreadAssigned,

    /// <summary>
    /// Inbox thread unassigned.
    /// </summary>
    InboxThreadUnassigned,

    /// <summary>
    /// Inbox thread labels.
    /// </summary>
    InboxThreadLabels,

    /// <summary>
    /// Inbox draft created, updated or deleted.
    /// </summary>
    InboxDraft,

    /// <summary>
    /// Inbox draft sent.
    /// </summary>
    InboxDraftSent,
}