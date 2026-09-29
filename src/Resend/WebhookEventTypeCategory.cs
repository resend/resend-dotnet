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
}