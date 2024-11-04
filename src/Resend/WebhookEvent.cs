﻿using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
/// <remarks>
/// See https://www.resend.com/docs/dashboard/webhooks/event-types
/// </remarks>
[JsonConverter( typeof( JsonStringEnumValueConverter<WebhookEvent> ) )]
public enum WebhookEvent
{
    /// <summary>
    /// The API request was successful and Resend will attempt to deliver
    /// the message to the recipient’s mail server.
    /// </summary>
    [JsonStringValue( "email.sent" )]
    EmailSent,

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
}
