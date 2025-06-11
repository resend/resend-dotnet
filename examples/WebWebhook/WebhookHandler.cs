using Resend.Webhooks;

namespace WebWebhook;

public class WebhookHandler : IWebhookHandler
{
    private readonly ILogger<WebhookHandler> _logger;


    public WebhookHandler( ILogger<WebhookHandler> logger )
    {
        _logger = logger;
    }


    /// <summary />
    public async Task<IResult> HandleInvalid( WebhookContext context )
    {
        await Task.Delay( 0 );

        return Results.NoContent();
    }


    /// <summary />
    public async Task<IResult> HandleValid( WebhookContext context )
    {
        await Task.Yield();

        _logger.LogInformation( "", context.MessageId, context.Event );

        return Results.NoContent();
    }
}