using Resend.Webhooks;
using System.Transactions;

namespace Resend.ApiServer.WebhookHandlers;

/// <summary />
public class Handler1 : IWebhookHandler
{
    private readonly ILogger<Handler1> _logger;

    /// <summary />
    public Handler1( ILogger<Handler1> logger )
    {
        _logger = logger;
    }


    /// <inheritdoc />
    public async Task<IResult> HandleValid( WebhookContext context )
    {
        await Task.Yield();

        _logger.LogInformation( "Handler 1: Always say Ok!" );


        /*
         * 
         */
        if ( context.IsValid == true )
            _logger.LogInformation( "Valid Webhook {MessageId}", context.MessageId );
        else
            _logger.LogWarning( "Invalid webhook {MessageId}: {ErrorCode}", context.MessageId, context.Exception?.ErrorCode );


        /*
         * 
         */
        var @event = context.Event;

        if ( context.IsValid == true && @event != null )
        {
            _logger.LogInformation( "Received {Category}: {EventType}", @event.EventType.Category(), @event.EventType );

            switch ( @event.EventType.Category() )
            {
                case WebhookEventTypeCategory.Email:
                    var eed = @event.DataAs<EmailEventData>();

                    _logger.LogDebug( "Email {EmailId}: {Subject}", eed.EmailId, eed.Subject );
                    break;

                case WebhookEventTypeCategory.Domain:
                    var ded = @event.DataAs<DomainEventData>();

                    _logger.LogDebug( "Domain {DomainId}: {Name}", ded.Id, ded.Name );
                    break;

                case WebhookEventTypeCategory.Contact:
                    var ced = @event.DataAs<ContactEventData>();

                    _logger.LogDebug( "Contact {ContactId}: {Email}", ced.ContactId, ced.Email );
                    break;

                case WebhookEventTypeCategory.Suppression:
                    var sed = @event.DataAs<SuppressionEventData>();

                    _logger.LogDebug( "Suppression {Id}: {Email}", sed.Id, sed.Email );
                    break;

                case WebhookEventTypeCategory.ContactTopics:
                    var cted = @event.DataAs<ContactTopicsEventData>();

                    _logger.LogDebug( "Contact topics {Email}: {Count}", cted.Email, cted.Topics?.Count ?? 0 );
                    break;

                case WebhookEventTypeCategory.Topic:
                    var ted = @event.DataAs<TopicEventData>();

                    _logger.LogDebug( "Topic {Id}: {Name}", ted.Id, ted.Name );
                    break;

                case WebhookEventTypeCategory.Inbox:
                    var ied = @event.DataAs<InboxEventData>();

                    _logger.LogDebug( "Inbox {InboxId}: {Source}", ied.InboxId, ied.Source );
                    break;

                case WebhookEventTypeCategory.InboxDeleted:
                    var idd = @event.DataAs<InboxDeletedEventData>();

                    _logger.LogDebug( "Inbox deleted {InboxId}: {Source}", idd.InboxId, idd.Source );
                    break;

                case WebhookEventTypeCategory.InboxThreadEmail:
                    var ited = @event.DataAs<InboxThreadEmailEventData>();

                    _logger.LogDebug( "Inbox thread {ThreadId}: {EmailId}", ited.ThreadId, ited.EmailId );
                    break;

                case WebhookEventTypeCategory.InboxThreadFolder:
                    var itfd = @event.DataAs<InboxThreadFolderUpdatedEventData>();

                    _logger.LogDebug( "Inbox thread {ThreadId}: {From} to {To}", itfd.ThreadId, itfd.From, itfd.To );
                    break;

                case WebhookEventTypeCategory.InboxThreadAssigned:
                    var itad = @event.DataAs<InboxThreadAssignedEventData>();

                    _logger.LogDebug( "Inbox thread {ThreadId}: assigned to {Assignee}", itad.ThreadId, itad.AssigneeEmail );
                    break;

                case WebhookEventTypeCategory.InboxThreadUnassigned:
                    var itud = @event.DataAs<InboxThreadUnassignedEventData>();

                    _logger.LogDebug( "Inbox thread {ThreadId}: unassigned from {Assignee}", itud.ThreadId, itud.PreviousAssigneeEmail );
                    break;

                case WebhookEventTypeCategory.InboxThreadLabels:
                    var itld = @event.DataAs<InboxThreadLabelsUpdatedEventData>();

                    _logger.LogDebug( "Inbox thread {ThreadId}: {Added} added, {Removed} removed", itld.ThreadId, itld.Added.Count, itld.Removed.Count );
                    break;

                case WebhookEventTypeCategory.InboxDraft:
                    var idrd = @event.DataAs<InboxDraftEventData>();

                    _logger.LogDebug( "Inbox draft {DraftId}: {Source}", idrd.DraftId, idrd.Source );
                    break;

                case WebhookEventTypeCategory.InboxDraftSent:
                    var idsd = @event.DataAs<InboxDraftSentEventData>();

                    _logger.LogDebug( "Inbox draft {DraftId}: sent as {EmailId}", idsd.DraftId, idsd.EmailId );
                    break;
            }
        }
        else
        {

        }


        if ( context.IsValid == true )
            return Results.Ok();
        else
            return Results.BadRequest();
    }


    /// <inheritdoc />
    public async Task<IResult> HandleInvalid( WebhookContext context )
    {
        await Task.Yield();

        _logger.LogError( "Invalid {MessageId}: {ErrorCode} - {Message}", context.MessageId, context.Exception?.ErrorCode, context.Exception?.Message );

        return Results.BadRequest();
    }
}