namespace Resend;

/// <summary>
/// Resend client.
/// </summary>
public interface IResend
{
    #region Email (Sending)

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
    /// Lists a page of email receipts.
    /// </summary>
    /// <param name="query">
    /// Pagination query.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// List of email receipts.
    /// </returns>
    Task<ResendResponse<PaginatedResult<EmailReceipt>>> EmailListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

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
    /// Lists email attachments from a sent email.
    /// </summary>
    /// <param name="emailId">Sent email identifier.</param>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of attachments.</returns>
    Task<ResendResponse<PaginatedResult<SentEmailAttachment>>> EmailAttachmentListAsync( Guid emailId, PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves an email attachment of a sent email.
    /// </summary>
    /// <param name="emailId">Sent email identifier.</param>
    /// <param name="attachmentId">Sent email attachment identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Email attachment.</returns>
    Task<ResendResponse<SentEmailAttachment>> EmailAttachmentRetrieveAsync( Guid emailId, Guid attachmentId, CancellationToken cancellationToken = default );

    #endregion

    #region Domains

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

    #endregion

    #region Api Keys

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

    #endregion

    #region Audiences

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
    [Obsolete( "Use Segments instead" )]
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
    [Obsolete( "Use Segments instead" )]
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
    [Obsolete( "Use Segments instead" )]
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
    [Obsolete( "Use Segments instead" )]
    Task<ResendResponse<List<Audience>>> AudienceListAsync( CancellationToken cancellationToken = default );

    #endregion

    #region Contacts

    /// <summary>
    /// Create a contact.
    /// </summary>
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
    Task<ResendResponse<Guid>> ContactAddAsync( ContactData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a contact.
    /// </summary>
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
    Task<ResendResponse<Contact>> ContactRetrieveAsync( Guid contactId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a single contact from an audience using email address.
    /// </summary>
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
    Task<ResendResponse<Contact>> ContactRetrieveByEmailAsync( string email, CancellationToken cancellationToken = default );

    /// <summary>
    /// Update an existing contact.
    /// </summary>
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
    Task<ResendResponse> ContactUpdateAsync( Guid contactId, ContactData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Update an existing contact.
    /// </summary>
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
    Task<ResendResponse> ContactUpdateByEmailAsync( string email, ContactData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Remove a contact.
    /// </summary>
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
    Task<ResendResponse> ContactDeleteAsync( Guid contactId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Remove a contact by their email address.
    /// </summary>
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
    Task<ResendResponse> ContactDeleteByEmailAsync( string email, CancellationToken cancellationToken = default );

    /// <summary>
    /// List contacts.
    /// </summary>
    /// <param name="query">
    /// Paginated query.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancelation token.
    /// </param>
    /// <returns>
    /// List of contacts.
    /// </returns>
    /// <see href="https://resend.com/docs/api-reference/contacts/list-contacts" />
    Task<ResendResponse<PaginatedResult<Contact>>> ContactListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Lists segments for a contact.
    /// </summary>
    /// <param name="contactId">Contact identifier.</param>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of segments.</returns>
    Task<ResendResponse<PaginatedResult<Segment>>> ContactListSegmentsAsync( Guid contactId, PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Adds a contact to a segment.
    /// </summary>
    /// <param name="contactId">Contact identifier.</param>
    /// <param name="segmentId">Segment identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> ContactAddToSegmentAsync( Guid contactId, Guid segmentId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Removes a contact from a segment.
    /// </summary>
    /// <param name="contactId">Contact identifier.</param>
    /// <param name="segmentId">Segment identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> ContactRemoveFromSegmentAsync( Guid contactId, Guid segmentId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a list of topics subscriptions for a contact.
    /// </summary>
    /// <param name="contactId">Contact identifier.</param>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of topic subscriptions.</returns>
    Task<ResendResponse<PaginatedResult<TopicSubscription>>> ContactListTopicsAsync( Guid contactId, PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Update topic subscriptions for a contact.
    /// </summary>
    /// <param name="contactId">Contact identifier.</param>
    /// <param name="topics">List of topic subscriptions.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result.</returns>
    Task<ResendResponse> ContactUpdateTopicsAsync( Guid contactId, List<TopicSubscription> topics, CancellationToken cancellationToken = default );

    #endregion

    #region Contact Properties

    /// <summary>
    /// List custom contact properties.
    /// </summary>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<ResendResponse<PaginatedResult<ContactProperty>>> ContactPropListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Creates a custom contact property.
    /// </summary>
    /// <param name="prop">Property data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<ResendResponse<Guid>> ContactPropCreateAsync( ContactPropertyData prop, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves a custom contact property.
    /// </summary>
    /// <param name="propId">Property identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<ResendResponse<ContactProperty>> ContactPropRetrieveAsync( Guid propId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates a custom contact property.
    /// </summary>
    /// <param name="propId">Property identifier.</param>
    /// <param name="prop">Property data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<ResendResponse> ContactPropUpdateAsync( Guid propId, ContactPropertyUpdateData prop, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes a custom contact property.
    /// </summary>
    /// <param name="propId">Property identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<ResendResponse> ContactPropDeleteAsync( Guid propId, CancellationToken cancellationToken = default );

    #endregion

    #region Broadcasts

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

    #endregion

    #region Receiving

    /// <summary>
    /// Lists received emails.
    /// </summary>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of received emails.</returns>
    Task<ResendResponse<PaginatedResult<ReceivedEmail>>> ReceivedEmailListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves a received email.
    /// </summary>
    /// <param name="emailId"></param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Received email.</returns>
    Task<ResendResponse<ReceivedEmail>> ReceivedEmailRetrieveAsync( Guid emailId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Lists attachments associated with a received email.
    /// </summary>
    /// <param name="emailId">Email identifier.</param>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of attachments.</returns>
    Task<ResendResponse<PaginatedResult<ReceivedEmailAttachment>>> ReceivedEmailAttachmentListAsync( Guid emailId, PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves an attachment from a received email.
    /// </summary>
    /// <param name="emailId">Email identifier.</param>
    /// <param name="attachmentId">Attachment identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Attachment.</returns>
    Task<ResendResponse<ReceivedEmailAttachment>> ReceivedEmailAttachmentRetrieveAsync( Guid emailId, Guid attachmentId, CancellationToken cancellationToken = default );

    #endregion

    #region Segments

    /// <summary>
    /// Lists segments.
    /// </summary>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of segments.</returns>
    Task<ResendResponse<PaginatedResult<Segment>>> SegmentListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Create a segment.
    /// </summary>
    /// <param name="segment">Segment data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Segment identifier.</returns>
    Task<ResendResponse<Guid>> SegmentCreateAsync( SegmentData segment, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a segment.
    /// </summary>
    /// <param name="segmentId">Segment identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Segment.</returns>
    Task<ResendResponse<Segment>> SegmentRetrieveAsync( Guid segmentId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes a segment.
    /// </summary>
    /// <param name="segmentId">Segment identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> SegmentDeleteAsync( Guid segmentId, CancellationToken cancellationToken = default );

    #endregion

    #region Templates

    /// <summary>
    /// Lists templates.
    /// </summary>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of templates.</returns>
    Task<ResendResponse<PaginatedResult<TemplateSummary>>> TemplateListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Create a template.
    /// </summary>
    /// <param name="template">Template data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Topic identifier.</returns>
    Task<ResendResponse<Guid>> TemplateCreateAsync( TemplateData template, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Template.</returns>
    Task<ResendResponse<Template>> TemplateRetrieveAsync( Guid templateId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a template, by alias.
    /// </summary>
    /// <param name="templateAlias">Template alias.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Template.</returns>
    Task<ResendResponse<Template>> TemplateRetrieveAsync( string templateAlias, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates a template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="template">Template data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TemplateUpdateAsync( Guid templateId, TemplateData template, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates a template, by alias.
    /// </summary>
    /// <param name="templateAlias">Template alias.</param>
    /// <param name="template">Template data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TemplateUpdateAsync( string templateAlias, TemplateData template, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes a template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TemplateDeleteAsync( Guid templateId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes a template, by alias.
    /// </summary>
    /// <param name="templateAlias">Template alias.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TemplateDeleteAsync( string templateAlias, CancellationToken cancellationToken = default );

    /// <summary>
    /// Publishes a template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TemplatePublishAsync( Guid templateId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Publishes a template, by alias.
    /// </summary>
    /// <param name="templateAlias">Template alias.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TemplatePublishAsync( string templateAlias, CancellationToken cancellationToken = default );

    /// <summary>
    /// Duplicates a template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Identifier of newly created duplicate.</returns>
    Task<ResendResponse<Guid>> TemplateDuplicateAsync( Guid templateId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Duplicates a template, by alias.
    /// </summary>
    /// <param name="templateAlias">Template alias.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Identifier of newly created duplicate.</returns>
    Task<ResendResponse<Guid>> TemplateDuplicateAsync( string templateAlias, CancellationToken cancellationToken = default );

    #endregion

    #region Topics

    /// <summary>
    /// Lists topics.
    /// </summary>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of topics.</returns>
    Task<ResendResponse<PaginatedResult<Topic>>> TopicListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Create a topic.
    /// </summary>
    /// <param name="topic">Topic data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Topic identifier.</returns>
    Task<ResendResponse<Guid>> TopicCreateAsync( TopicData topic, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieve a topic.
    /// </summary>
    /// <param name="topicId">Topic identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Topic.</returns>
    Task<ResendResponse<Topic>> TopicRetrieveAsync( Guid topicId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates a topic.
    /// </summary>
    /// <param name="topicId">Topic identifier.</param>
    /// <param name="topic">Topic data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TopicUpdateAsync( Guid topicId, TopicData topic, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes a topic.
    /// </summary>
    /// <param name="topicId">Topic identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> TopicDeleteAsync( Guid topicId, CancellationToken cancellationToken = default );

    #endregion

    #region Webhooks

    /// <summary>
    /// Lists webhooks.
    /// </summary>
    /// <param name="query">Paginated query.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>List of webhooks.</returns>
    Task<ResendResponse<PaginatedResult<Webhook>>> WebhookListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Creates a webhook.
    /// </summary>
    /// <param name="webhook">Webhook data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse<WebhookNew>> WebhookCreateAsync( WebhookData webhook, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves a webhook.
    /// </summary>
    /// <param name="webhookId">Webhook identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse<Webhook>> WebhookRetrieveAsync( Guid webhookId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates a webhook.
    /// </summary>
    /// <param name="webhookId">Webhook identifier.</param>
    /// <param name="webhook">Webhook data.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> WebhookUpdateAsync( Guid webhookId, WebhookData webhook, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes a webhook.
    /// </summary>
    /// <param name="webhookId">Webhook identifier.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Response.</returns>
    Task<ResendResponse> WebhookDeleteAsync( Guid webhookId, CancellationToken cancellationToken = default );

    #endregion

    #region Logs

    /// <summary>
    /// Lists API request logs.
    /// </summary>
    /// <param name="query">Pagination query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page of logs.</returns>
    /// <see href="https://resend.com/docs/api-reference/logs/list-logs"/>
    Task<ResendResponse<PaginatedResult<Log>>> LogListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves a single API request log, including request and response bodies when available.
    /// </summary>
    /// <param name="logId">Log identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Log entry.</returns>
    /// <see href="https://resend.com/docs/api-reference/logs/retrieve-log"/>
    Task<ResendResponse<Log>> LogRetrieveAsync( Guid logId, CancellationToken cancellationToken = default );

    #endregion

    #region Automations and events

    /// <summary>
    /// Creates an automation.
    /// </summary>
    /// <param name="data">Automation definition.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>New automation identifier.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/create-automation"/>
    Task<ResendResponse<Guid>> AutomationCreateAsync( AutomationCreateData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates an automation.
    /// </summary>
    /// <param name="automationId">Automation identifier.</param>
    /// <param name="data">Fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Automation identifier.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/update-automation"/>
    Task<ResendResponse<Guid>> AutomationUpdateAsync( Guid automationId, AutomationUpdateData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves an automation by identifier.
    /// </summary>
    /// <param name="automationId">Automation identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Automation.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/retrieve-automation"/>
    Task<ResendResponse<Automation>> AutomationRetrieveAsync( Guid automationId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Lists automations.
    /// </summary>
    /// <param name="query">Filters and pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page of automations.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/list-automations"/>
    Task<ResendResponse<PaginatedResult<AutomationSummary>>> AutomationListAsync( AutomationListQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Stops a running automation (sets status to <c>disabled</c>).
    /// </summary>
    /// <param name="automationId">Automation identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result including updated status.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/stop-automation"/>
    Task<ResendResponse<AutomationStopResult>> AutomationStopAsync( Guid automationId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes an automation.
    /// </summary>
    /// <param name="automationId">Automation identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deletion confirmation.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/delete-automation"/>
    Task<ResendResponse<AutomationDeleteResult>> AutomationDeleteAsync( Guid automationId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Lists runs for an automation.
    /// </summary>
    /// <param name="automationId">Automation identifier.</param>
    /// <param name="query">Filters and pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page of runs.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/list-automation-runs"/>
    Task<ResendResponse<PaginatedResult<AutomationRunSummary>>> AutomationRunListAsync( Guid automationId, AutomationRunListQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves a single automation run.
    /// </summary>
    /// <param name="automationId">Automation identifier.</param>
    /// <param name="runId">Run identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Run detail.</returns>
    /// <see href="https://resend.com/docs/api-reference/automations/retrieve-automation-run"/>
    Task<ResendResponse<AutomationRun>> AutomationRunRetrieveAsync( Guid automationId, Guid runId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Creates an event definition (name and optional payload schema).
    /// </summary>
    /// <param name="data">Event name and optional schema.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>New event identifier (HTTP 201).</returns>
    /// <see href="https://resend.com/docs/api-reference/events/create-event"/>
    Task<ResendResponse<Guid>> EventCreateAsync( EventCreateData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves an event by identifier.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Event definition.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/retrieve-event"/>
    Task<ResendResponse<EventResource>> EventRetrieveAsync( Guid eventId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Retrieves an event by identifier or name.
    /// </summary>
    /// <param name="eventIdOrName">Event id (UUID) or name (for example <c>user.created</c>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Event definition.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/retrieve-event"/>
    Task<ResendResponse<EventResource>> EventRetrieveAsync( string eventIdOrName, CancellationToken cancellationToken = default );

    /// <summary>
    /// Lists event definitions.
    /// </summary>
    /// <param name="query">Pagination query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Page of events.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/list-events"/>
    Task<ResendResponse<PaginatedResult<EventResource>>> EventListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates an event definition (for example its payload schema).
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="data">Fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ID of the updated event.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/update-event"/>
    Task<ResendResponse<Guid>> EventUpdateAsync( Guid eventId, EventUpdateData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Updates an event definition by identifier or name.
    /// </summary>
    /// <param name="eventIdOrName">Event id (UUID) or name.</param>
    /// <param name="data">Fields to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ID of the updated event.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/update-event"/>
    Task<ResendResponse<Guid>> EventUpdateAsync( string eventIdOrName, EventUpdateData data, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes an event definition.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Delete result including the event id and confirmation flag.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/delete-event"/>
    Task<ResendResponse<EventDeleteResult>> EventDeleteAsync( Guid eventId, CancellationToken cancellationToken = default );

    /// <summary>
    /// Deletes an event definition by identifier or name.
    /// </summary>
    /// <param name="eventIdOrName">Event id (UUID) or name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Delete result including the event id and confirmation flag.</returns>
    /// <see href="https://resend.com/docs/api-reference/events/delete-event"/>
    Task<ResendResponse<EventDeleteResult>> EventDeleteAsync( string eventIdOrName, CancellationToken cancellationToken = default );

    /// <summary>
    /// Sends a named event (for example to trigger automations).
    /// </summary>
    /// <param name="data">Event name, contact or email, and optional payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Accepted event metadata (HTTP 202).</returns>
    /// <see href="https://resend.com/docs/api-reference/events/send-event"/>
    Task<ResendResponse<EventSendResult>> EventSendAsync( EventSendData data, CancellationToken cancellationToken = default );

    #endregion
}