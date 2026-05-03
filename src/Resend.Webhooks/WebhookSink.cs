using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Resend.Webhooks;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class WebhookSink<T>
    where T : notnull, IWebhookHandler
{
    /// <summary />
    public static async Task<IResult> ExecuteAsync(
        HttpRequest request,
        [FromServices] ILogger<WebhookSink<T>> logger,
        [FromServices] T handler,
        [FromServices] WebhookValidator validator,
        CancellationToken cancellationToken = default )
    {
        /*
         * 
         */
        var ctx = await validator.ValidateAsync( request, cancellationToken );


        /*
         * 
         */
        IResult r;

        try
        {
            if ( ctx.IsValid == true )
                r = await handler.HandleValid( ctx );
            else
                r = await handler.HandleInvalid( ctx );
        }
        catch ( Exception ex )
        {
            logger.LogError( ex, "Exception handling {MessageId}", ctx.MessageId );

            r = Results.StatusCode( (int) HttpStatusCode.InternalServerError );
        }

        logger.LogDebug( "Handling {MessageId}: {Result}", ctx.MessageId, r );

        return r;
    }
}