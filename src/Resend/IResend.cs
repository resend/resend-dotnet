namespace Resend;

/// <summary>
/// Resend client.
/// </summary>
public interface IResend
{
    /// <summary>
    /// Send an email.
    /// </summary>
    /// <param name="email">
    /// Email.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Email identifier.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/send-email"/>
    Task<ResendResponse<Guid>> EmailSendAsync( EmailMessage email, CancellationToken cancellationToken = default );


    /// <summary>
    /// Send an email using idempotency key, such that retries do not yield duplicate
    /// submissions.
    /// </summary>
    /// <param name="idempotencyKey">
    /// Idempotency key.
    /// </param>
    /// <param name="email">
    /// Email.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Email identifier.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/send-email"/>
    Task<ResendResponse<Guid>> EmailSendAsync( string idempotencyKey, EmailMessage email, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieves the email receipt for the given email.
    /// </summary>
    /// <param name="emailId">
    /// Email identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Email receipt.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/retrieve-email"/>
    Task<ResendResponse<EmailReceipt>> EmailRetrieveAsync( Guid emailId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Send a batch of emails, if and only if all messages are valid.
    /// </summary>
    /// <param name="emails">
    /// List of emails.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// List of email identifiers.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/send-batch-emails"/>
    Task<ResendResponse<List<Guid>>> EmailBatchAsync( IEnumerable<EmailMessage> emails, CancellationToken cancellationToken = default );


    /// <summary>
    /// Send a batch of emails using idempotency key, such that retries do not yield
    /// duplicate submissions. Emails are only send if and only if all messages are
    /// valid.
    /// </summary>
    /// <param name="idempotencyKey">
    /// Idempotency key.
    /// </param>
    /// <param name="emails">
    /// List of emails.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// List of email identifiers.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/send-batch-emails"/>
    Task<ResendResponse<List<Guid>>> EmailBatchAsync( string idempotencyKey, IEnumerable<EmailMessage> emails, CancellationToken cancellationToken = default );


    /// <summary>
    /// Send a batch of emails.
    /// </summary>
    /// <param name="emails">
    /// List of emails.
    /// </param>
    /// <param name="validationMode">
    /// Validation mode.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// List of email identifiers.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/send-batch-emails"/>
    Task<ResendResponse<EmailBatchResponse>> EmailBatchAsync( IEnumerable<EmailMessage> emails, EmailBatchValidationMode validationMode, CancellationToken cancellationToken = default );


    /// <summary>
    /// Send a batch of emails.
    /// </summary>
    /// <param name="idempotencyKey">
    /// Idempotency key.
    /// </param>
    /// <param name="emails">
    /// List of emails.
    /// </param>
    /// <param name="validationMode">
    /// Validation mode.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// List of email identifiers.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/emails/send-batch-emails"/>
    Task<ResendResponse<EmailBatchResponse>> EmailBatchAsync( string idempotencyKey, IEnumerable<EmailMessage> emails, EmailBatchValidationMode validationMode, CancellationToken cancellationToken = default );


    /// <summary>
    /// Reschedule an email.
    /// </summary>
    /// <param name="emailId">
    /// Email identifier.
    /// </param>
    /// <param name="rescheduleFor">
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <see href="https://www.resend.com/docs/api-reference/emails/update-email"/>
    Task<ResendResponse> EmailRescheduleAsync( Guid emailId, DateTimeOrHuman rescheduleFor, CancellationToken cancellationToken = default );


    /// <summary>
    /// Cancel a scheduled email.
    /// </summary>
    /// <param name="emailId">
    /// Email identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <see href="https://www.resend.com/docs/api-reference/emails/cancel-email"/>
    Task<ResendResponse> EmailCancelAsync( Guid emailId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieve a list of domains for the authenticated user.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Domain.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/list-domains"/>
    Task<ResendResponse<List<Domain>>> DomainListAsync( CancellationToken cancellationToken = default );


    /// <summary>
    /// Create a (sender) domain.
    /// </summary>
    /// <param name="domainName">
    /// Domain DNS name.
    /// </param>
    /// <param name="region">
    /// Delivery region.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Domain.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/create-domain"/>
    Task<ResendResponse<Domain>> DomainAddAsync( string domainName, DeliveryRegion? region = null, CancellationToken cancellationToken = default );


    /// <summary>
    /// Create a (sender) domain.
    /// </summary>
    /// <param name="data">
    /// Domain name.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Domain.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/create-domain"/>
    Task<ResendResponse<Domain>> DomainAddAsync( DomainAddData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieves a domain.
    /// </summary>
    /// <param name="domainId">
    /// Domain identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Domain.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/get-domain"/>
    Task<ResendResponse<Domain>> DomainRetrieveAsync( Guid domainId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Update an existing domain.
    /// </summary>
    /// <param name="domainId">
    /// Domain identifier.
    /// </param>
    /// <param name="data">
    /// Updated domain information.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Domain.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/update-domain"/>
    Task<ResendResponse> DomainUpdateAsync( Guid domainId, DomainUpdateData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Verify an existing domain.
    /// </summary>
    /// <param name="domainId">
    /// Domain identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/verify-domain"/>
    Task<ResendResponse> DomainVerifyAsync( Guid domainId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Remove an existing domain.
    /// </summary>
    /// <param name="domainId">
    /// Domain identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/domains/delete-domain"/>
    Task<ResendResponse> DomainDeleteAsync( Guid domainId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Lists all defined API keys.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>List of API keys</returns>
    /// <see href="https://resend.com/docs/api-reference/api-keys/list-api-keys"/>
    Task<ResendResponse<List<ApiKey>>> ApiKeyListAsync( CancellationToken cancellationToken = default );


    /// <summary>
    /// Add a new API key to authenticate communications with Resend.
    /// </summary>
    /// <param name="keyName">
    /// Name of the API key.
    /// </param>
    /// <param name="permission">
    /// Permissions that apply to the API key.
    /// </param>
    /// <param name="domainId">
    /// Restrict an API key to send emails only from a specific domain. Only used when
    /// the permission is FullAccess.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// API key data. The token is only available during creation!
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/api-keys/create-api-key"/>
    Task<ResendResponse<ApiKeyData>> ApiKeyCreateAsync( string keyName, Permission? permission = null, Guid? domainId = null, CancellationToken cancellationToken = default );


    /// <summary>
    /// Remove an existing API key.
    /// </summary>
    /// <param name="apiKeyId">
    /// API key identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/api-keys/delete-api-key" />
    Task<ResendResponse> ApiKeyDeleteAsync( Guid apiKeyId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Create a list of contacts.
    /// </summary>
    /// <param name="name">
    /// The name of the audience you want to create.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/audiences/create-audience" />
    Task<ResendResponse<Guid>> AudienceAddAsync( string name, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieve a single audience.
    /// </summary>
    /// /// <param name="audienceId">
    /// The audience identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// List of Audience.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/audiences/get-audience" />
    Task<ResendResponse<Audience>> AudienceRetrieveAsync( Guid audienceId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Remove an existing audience.
    /// </summary>
    /// <param name="audienceId">
    /// The Audience ID.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/audiences/delete-audience" />
    Task<ResendResponse> AudienceDeleteAsync( Guid audienceId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieve a list of audiences.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A list of Audience.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/audiences/list-audiences" />
    Task<ResendResponse<List<Audience>>> AudienceListAsync( CancellationToken cancellationToken = default );


    /// <summary>
    /// Create a contact inside an audience.
    /// </summary>
    /// /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="data">
    /// Contact data.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// Contact Id. 
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/create-contact" />
    Task<ResendResponse<Guid>> ContactAddAsync( Guid audienceId, ContactData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieve a single contact from an audience.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="contactId">
    /// Contact identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// A Contact.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/get-contact" />
    Task<ResendResponse<Contact>> ContactRetrieveAsync( Guid audienceId, Guid contactId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieve a single contact from an audience using email address.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="email">
    /// Contact email.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// A Contact.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/get-contact" />
    Task<ResendResponse<Contact>> ContactRetrieveByEmailAsync( Guid audienceId, string email, CancellationToken cancellationToken = default );


    /// <summary>
    /// Update an existing contact.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="contactId">
    /// Contact identifier.
    /// </param>
    /// <param name="data">
    /// Contact data.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/update-contact" />
    Task<ResendResponse> ContactUpdateAsync( Guid audienceId, Guid contactId, ContactData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Update an existing contact.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="email">
    /// Email.
    /// </param>
    /// <param name="data">
    /// Contact data.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/update-contact" />
    Task<ResendResponse> ContactUpdateByEmailAsync( Guid audienceId, string email, ContactData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Remove a contact.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="contactId">
    /// Contact identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// Task.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/delete-contact" />
    Task<ResendResponse> ContactDeleteAsync( Guid audienceId, Guid contactId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Remove a contact by their email address.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="email">
    /// Email address of the contact.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// Task
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/delete-contact" />
    Task<ResendResponse> ContactDeleteByEmailAsync( Guid audienceId, string email, CancellationToken cancellationToken = default );



    /// <summary>
    /// List all contacts from an audience.
    /// </summary>
    /// <param name="audienceId">
    /// Audience identifier.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// List of contacts. 
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/list-contacts" />
    Task<ResendResponse<List<Contact>>> ContactListAsync( Guid audienceId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Create a broadcast.
    /// </summary>
    /// <param name="data">Broadcast data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Broadcast identifier.</returns>
    Task<ResendResponse<Guid>> BroadcastAddAsync( BroadcastData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Retrieve a broadcast.
    /// </summary>
    /// <param name="broadcastId">Broadcast identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Broadcast data.</returns>
    Task<ResendResponse<Broadcast>> BroadcastRetrieveAsync( Guid broadcastId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Updates a draft broadcast.
    /// </summary>
    /// <param name="broadcastId">Broadcast identifier.</param>
    /// <param name="data">Broadcast data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> BroadcastUpdateAsync( Guid broadcastId, BroadcastUpdateData data, CancellationToken cancellationToken = default );


    /// <summary>
    /// Sends a broadcast immediately.
    /// </summary>
    /// <param name="broadcastId">Broadcast identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> BroadcastSendAsync( Guid broadcastId, CancellationToken cancellationToken = default );


    /// <summary>
    /// Schedules a broadcast for a future moment.
    /// </summary>
    /// <param name="broadcastId">Broadcast identifier.</param>
    /// <param name="scheduleFor">Moment for which broadcast is being scheduled for.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> BroadcastScheduleAsync( Guid broadcastId, DateTime scheduleFor, CancellationToken cancellationToken = default );


    /// <summary>
    /// Lists all broadcasts.
    /// </summary>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of broadcasts</returns>
    Task<ResendResponse<List<Broadcast>>> BroadcastListAsync( CancellationToken cancellationToken = default );


    /// <summary>
    /// Removes a broadcast.
    /// </summary>
    /// <param name="broadcastId">Broadcast identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> BroadcastDeleteAsync( Guid broadcastId, CancellationToken cancellationToken = default );
}
