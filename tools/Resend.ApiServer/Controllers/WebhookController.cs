using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

[ApiController]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> _logger;


    public WebhookController( ILogger<WebhookController> logger )
    {
        _logger = logger;
    }


    [HttpPost]
    [Route( "webhooks" )]
    public WebhookAddResponse WebhookAdd( [FromBody] WebhookAddData data )
    {
        _logger.LogDebug( "WebhookAdd" );

        return new WebhookAddResponse()
        {
            Object = "webhook",
            Id = Guid.NewGuid(),
            SigningSecret = "whsec_test_secret_12345",
        };
    }


    [HttpGet]
    [Route( "webhooks/{webhookId}" )]
    public Webhook WebhookRetrieve( Guid webhookId )
    {
        _logger.LogDebug( "WebhookRetrieve" );

        return new Webhook()
        {
            Id = webhookId,
            EndpointUrl = "https://example.com/webhook",
            MomentCreated = DateTime.UtcNow,
            Status = WebhookStatus.Enabled,
            Events = new List<WebhookEventType>()
            {
                WebhookEventType.EmailSent,
                WebhookEventType.EmailDelivered,
            },
            SecretKey = "whsec_test_secret_12345",
            SvixEndpointId = "ep_test123",
        };
    }


    [HttpGet]
    [Route( "webhooks" )]
    public PaginatedResult<Webhook> WebhookList()
    {
        _logger.LogDebug( "WebhookList" );

        return new PaginatedResult<Webhook>()
        {
            HasMore = false,
            Data = new List<Webhook>()
            {
                new Webhook()
                {
                    Id = Guid.NewGuid(),
                    EndpointUrl = "https://example.com/webhook1",
                    MomentCreated = DateTime.UtcNow,
                    Status = WebhookStatus.Enabled,
                    Events = new List<WebhookEventType>()
                    {
                        WebhookEventType.EmailSent,
                    },
                },
                new Webhook()
                {
                    Id = Guid.NewGuid(),
                    EndpointUrl = "https://example.com/webhook2",
                    MomentCreated = DateTime.UtcNow,
                    Status = WebhookStatus.Disabled,
                    Events = new List<WebhookEventType>()
                    {
                        WebhookEventType.EmailDelivered,
                        WebhookEventType.EmailBounced,
                    },
                }
            },
        };
    }


    [HttpPatch]
    [Route( "webhooks/{webhookId}" )]
    public ActionResult WebhookUpdate( [FromRoute] Guid webhookId, [FromBody] WebhookUpdateData data )
    {
        _logger.LogDebug( "WebhookUpdate" );

        return Ok();
    }


    [HttpDelete]
    [Route( "webhooks/{webhookId}" )]
    public ActionResult WebhookDelete( [FromRoute] Guid webhookId )
    {
        _logger.LogDebug( "WebhookDelete" );

        return Ok();
    }
}
