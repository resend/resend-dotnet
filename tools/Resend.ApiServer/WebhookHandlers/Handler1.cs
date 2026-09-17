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