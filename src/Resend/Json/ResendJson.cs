using Resend.Payloads;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Resend;

/// <summary>
/// Typed serialization metadata for Resend payloads.
/// </summary>
/// <remarks>
/// Type infos are bound to Web serializer defaults; when reflection-based
/// serialization is enabled (the default for JIT-compiled applications), a
/// reflection resolver is chained after the generated metadata, so arbitrary
/// runtime types remain supported inside <c>object</c> properties.
/// </remarks>
internal static class ResendJson
{
    private static JsonSerializerOptions Options { get; } = CreateOptions();


    private static JsonSerializerOptions CreateOptions()
    {
        var options = new JsonSerializerOptions( JsonSerializerDefaults.Web );

        if ( JsonSerializer.IsReflectionEnabledByDefault )
            options.TypeInfoResolver = JsonTypeInfoResolver.Combine( ResendJsonContext.Default, CreateReflectionResolver() );
        else
            options.TypeInfoResolver = ResendJsonContext.Default;

        return options;
    }


    [UnconditionalSuppressMessage( "Trimming", "IL2026", Justification = "Only used when reflection-based serialization is enabled (never in trimmed/AOT apps)." )]
    [UnconditionalSuppressMessage( "AOT", "IL3050", Justification = "Only used when reflection-based serialization is enabled (never in trimmed/AOT apps)." )]
    private static IJsonTypeInfoResolver CreateReflectionResolver()
    {
        return new DefaultJsonTypeInfoResolver();
    }


    private static JsonTypeInfo<T> Of<T>()
    {
        return (JsonTypeInfo<T>) Options.GetTypeInfo( typeof( T ) )!;
    }


    /// <summary />
    internal static JsonTypeInfo<ApiKeyCreateRequest> ApiKeyCreateRequest => Of<ApiKeyCreateRequest>();

    /// <summary />
    internal static JsonTypeInfo<ApiKeyData> ApiKeyData => Of<ApiKeyData>();

    /// <summary />
    internal static JsonTypeInfo<ApiKeyUpdateRequest> ApiKeyUpdateRequest => Of<ApiKeyUpdateRequest>();

    /// <summary />
    internal static JsonTypeInfo<Audience> Audience => Of<Audience>();

    /// <summary />
    internal static JsonTypeInfo<AudienceAddRequest> AudienceAddRequest => Of<AudienceAddRequest>();

    /// <summary />
    internal static JsonTypeInfo<Automation> Automation => Of<Automation>();

    /// <summary />
    internal static JsonTypeInfo<AutomationCreateData> AutomationCreateData => Of<AutomationCreateData>();

    /// <summary />
    internal static JsonTypeInfo<AutomationDeleteResult> AutomationDeleteResult => Of<AutomationDeleteResult>();

    /// <summary />
    internal static JsonTypeInfo<AutomationRun> AutomationRun => Of<AutomationRun>();

    /// <summary />
    internal static JsonTypeInfo<AutomationStopResult> AutomationStopResult => Of<AutomationStopResult>();

    /// <summary />
    internal static JsonTypeInfo<AutomationUpdateData> AutomationUpdateData => Of<AutomationUpdateData>();

    /// <summary />
    internal static JsonTypeInfo<Broadcast> Broadcast => Of<Broadcast>();

    /// <summary />
    internal static JsonTypeInfo<BroadcastData> BroadcastData => Of<BroadcastData>();

    /// <summary />
    internal static JsonTypeInfo<BroadcastScheduleRequest> BroadcastScheduleRequest => Of<BroadcastScheduleRequest>();

    /// <summary />
    internal static JsonTypeInfo<BroadcastUpdateData> BroadcastUpdateData => Of<BroadcastUpdateData>();

    /// <summary />
    internal static JsonTypeInfo<Contact> Contact => Of<Contact>();

    /// <summary />
    internal static JsonTypeInfo<ContactData> ContactData => Of<ContactData>();

    /// <summary />
    internal static JsonTypeInfo<ContactProperty> ContactProperty => Of<ContactProperty>();

    /// <summary />
    internal static JsonTypeInfo<ContactPropertyData> ContactPropertyData => Of<ContactPropertyData>();

    /// <summary />
    internal static JsonTypeInfo<ContactPropertyUpdateData> ContactPropertyUpdateData => Of<ContactPropertyUpdateData>();

    /// <summary />
    internal static JsonTypeInfo<Domain> Domain => Of<Domain>();

    /// <summary />
    internal static JsonTypeInfo<DomainAddData> DomainAddData => Of<DomainAddData>();

    /// <summary />
    internal static JsonTypeInfo<DomainClaim> DomainClaim => Of<DomainClaim>();

    /// <summary />
    internal static JsonTypeInfo<DomainClaimData> DomainClaimData => Of<DomainClaimData>();

    /// <summary />
    internal static JsonTypeInfo<DomainUpdateData> DomainUpdateData => Of<DomainUpdateData>();

    /// <summary />
    internal static JsonTypeInfo<EmailBatchResponse> EmailBatchResponse => Of<EmailBatchResponse>();

    /// <summary />
    internal static JsonTypeInfo<EmailMessage> EmailMessage => Of<EmailMessage>();

    /// <summary />
    internal static JsonTypeInfo<EmailMetrics> EmailMetrics => Of<EmailMetrics>();

    /// <summary />
    internal static JsonTypeInfo<EmailReceipt> EmailReceipt => Of<EmailReceipt>();

    /// <summary />
    internal static JsonTypeInfo<EmailRescheduleRequest> EmailRescheduleRequest => Of<EmailRescheduleRequest>();

    /// <summary />
    internal static JsonTypeInfo<EmailShareRequest> EmailShareRequest => Of<EmailShareRequest>();

    /// <summary />
    internal static JsonTypeInfo<EmailShareResult> EmailShareResult => Of<EmailShareResult>();

    /// <summary />
    internal static JsonTypeInfo<ErrorResponse> ErrorResponse => Of<ErrorResponse>();

    /// <summary />
    internal static JsonTypeInfo<EventCreateData> EventCreateData => Of<EventCreateData>();

    /// <summary />
    internal static JsonTypeInfo<EventDeleteResult> EventDeleteResult => Of<EventDeleteResult>();

    /// <summary />
    internal static JsonTypeInfo<EventResource> EventResource => Of<EventResource>();

    /// <summary />
    internal static JsonTypeInfo<EventSendData> EventSendData => Of<EventSendData>();

    /// <summary />
    internal static JsonTypeInfo<EventSendResult> EventSendResult => Of<EventSendResult>();

    /// <summary />
    internal static JsonTypeInfo<EventUpdateData> EventUpdateData => Of<EventUpdateData>();

    /// <summary />
    internal static JsonTypeInfo<IEnumerable<EmailMessage>> IEnumerableEmailMessage => Of<IEnumerable<EmailMessage>>();

    /// <summary />
    internal static JsonTypeInfo<ListOf<ApiKey>> ListOfApiKey => Of<ListOf<ApiKey>>();

    /// <summary />
    internal static JsonTypeInfo<ListOf<Audience>> ListOfAudience => Of<ListOf<Audience>>();

    /// <summary />
    internal static JsonTypeInfo<ListOf<Broadcast>> ListOfBroadcast => Of<ListOf<Broadcast>>();

    /// <summary />
    internal static JsonTypeInfo<ListOf<Domain>> ListOfDomain => Of<ListOf<Domain>>();

    /// <summary />
    internal static JsonTypeInfo<ListOf<ObjectId>> ListOfObjectId => Of<ListOf<ObjectId>>();

    /// <summary />
    internal static JsonTypeInfo<ListOf<SuppressionRemoveResult>> ListOfSuppressionRemoveResult => Of<ListOf<SuppressionRemoveResult>>();

    /// <summary />
    internal static JsonTypeInfo<List<TopicSubscription>> ListTopicSubscription => Of<List<TopicSubscription>>();

    /// <summary />
    internal static JsonTypeInfo<Log> Log => Of<Log>();

    /// <summary />
    internal static JsonTypeInfo<OAuthGrantRevoked> OAuthGrantRevoked => Of<OAuthGrantRevoked>();

    /// <summary />
    internal static JsonTypeInfo<ObjectId> ObjectId => Of<ObjectId>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<AutomationRunSummary>> PaginatedResultAutomationRunSummary => Of<PaginatedResult<AutomationRunSummary>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<AutomationSummary>> PaginatedResultAutomationSummary => Of<PaginatedResult<AutomationSummary>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<BroadcastClickedLink>> PaginatedResultBroadcastClickedLink => Of<PaginatedResult<BroadcastClickedLink>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<BroadcastRecipient>> PaginatedResultBroadcastRecipient => Of<PaginatedResult<BroadcastRecipient>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<Contact>> PaginatedResultContact => Of<PaginatedResult<Contact>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<ContactProperty>> PaginatedResultContactProperty => Of<PaginatedResult<ContactProperty>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<EmailReceipt>> PaginatedResultEmailReceipt => Of<PaginatedResult<EmailReceipt>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<EventResource>> PaginatedResultEventResource => Of<PaginatedResult<EventResource>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<Log>> PaginatedResultLog => Of<PaginatedResult<Log>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<OAuthGrant>> PaginatedResultOAuthGrant => Of<PaginatedResult<OAuthGrant>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<ReceivedEmail>> PaginatedResultReceivedEmail => Of<PaginatedResult<ReceivedEmail>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<ReceivedEmailAttachment>> PaginatedResultReceivedEmailAttachment => Of<PaginatedResult<ReceivedEmailAttachment>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<Segment>> PaginatedResultSegment => Of<PaginatedResult<Segment>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<SentEmailAttachment>> PaginatedResultSentEmailAttachment => Of<PaginatedResult<SentEmailAttachment>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<SuppressionSummary>> PaginatedResultSuppressionSummary => Of<PaginatedResult<SuppressionSummary>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<TemplateSummary>> PaginatedResultTemplateSummary => Of<PaginatedResult<TemplateSummary>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<Topic>> PaginatedResultTopic => Of<PaginatedResult<Topic>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<TopicSubscription>> PaginatedResultTopicSubscription => Of<PaginatedResult<TopicSubscription>>();

    /// <summary />
    internal static JsonTypeInfo<PaginatedResult<Webhook>> PaginatedResultWebhook => Of<PaginatedResult<Webhook>>();

    /// <summary />
    internal static JsonTypeInfo<ReceivedEmail> ReceivedEmail => Of<ReceivedEmail>();

    /// <summary />
    internal static JsonTypeInfo<ReceivedEmailAttachment> ReceivedEmailAttachment => Of<ReceivedEmailAttachment>();

    /// <summary />
    internal static JsonTypeInfo<Segment> Segment => Of<Segment>();

    /// <summary />
    internal static JsonTypeInfo<SegmentData> SegmentData => Of<SegmentData>();

    /// <summary />
    internal static JsonTypeInfo<SegmentUpdateResult> SegmentUpdateResult => Of<SegmentUpdateResult>();

    /// <summary />
    internal static JsonTypeInfo<SentEmailAttachment> SentEmailAttachment => Of<SentEmailAttachment>();

    /// <summary />
    internal static JsonTypeInfo<Suppression> Suppression => Of<Suppression>();

    /// <summary />
    internal static JsonTypeInfo<SuppressionAddRequest> SuppressionAddRequest => Of<SuppressionAddRequest>();

    /// <summary />
    internal static JsonTypeInfo<SuppressionBatchAddRequest> SuppressionBatchAddRequest => Of<SuppressionBatchAddRequest>();

    /// <summary />
    internal static JsonTypeInfo<SuppressionBatchRemoveRequest> SuppressionBatchRemoveRequest => Of<SuppressionBatchRemoveRequest>();

    /// <summary />
    internal static JsonTypeInfo<SuppressionRemoveResult> SuppressionRemoveResult => Of<SuppressionRemoveResult>();

    /// <summary />
    internal static JsonTypeInfo<Template> Template => Of<Template>();

    /// <summary />
    internal static JsonTypeInfo<TemplateData> TemplateData => Of<TemplateData>();

    /// <summary />
    internal static JsonTypeInfo<Topic> Topic => Of<Topic>();

    /// <summary />
    internal static JsonTypeInfo<TopicData> TopicData => Of<TopicData>();

    /// <summary />
    internal static JsonTypeInfo<Usage> Usage => Of<Usage>();

    /// <summary />
    internal static JsonTypeInfo<Webhook> Webhook => Of<Webhook>();

    /// <summary />
    internal static JsonTypeInfo<WebhookData> WebhookData => Of<WebhookData>();

    /// <summary />
    internal static JsonTypeInfo<WebhookEventAttemptListResult> WebhookEventAttemptListResult => Of<WebhookEventAttemptListResult>();

    /// <summary />
    internal static JsonTypeInfo<WebhookEventDetails> WebhookEventDetails => Of<WebhookEventDetails>();

    /// <summary />
    internal static JsonTypeInfo<WebhookEventListResult> WebhookEventListResult => Of<WebhookEventListResult>();

    /// <summary />
    internal static JsonTypeInfo<WebhookNew> WebhookNew => Of<WebhookNew>();
}
