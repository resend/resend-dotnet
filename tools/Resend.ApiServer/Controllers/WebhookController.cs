using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> _logger;


    /// <summary />
    public WebhookController( ILogger<WebhookController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "webhooks" )]
    public ObjectId WebhookAdd( [FromBody] WebhookData request )
    {
        _logger.LogDebug( "WebhookAdd" );

        return new ObjectId()
        {
            Object = "webhook",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "webhooks/{id}" )]
    public Webhook WebhookRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "WebhookRetrieve" );

        return new Webhook()
        {
            Id = Guid.NewGuid(),
            EndpointUrl = "https://domain.name/sink/",
            Status = WebhookStatus.Enabled,
            Events = [ WebhookEventType.EmailDelivered ],
            SigningSecret = "random-secret",
            MomentCreated = DateTime.UtcNow,
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "webhooks/{id}" )]
    public ObjectId WebhookUpdate( [FromRoute] Guid id, [FromBody] WebhookData data )
    {
        _logger.LogDebug( "WebhookUpdate" );

        return new ObjectId()
        {
            Object = "webhook",
            Id = id,
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "webhooks/{id}" )]
    public ActionResult WebhookDelete( [FromRoute] Guid id )
    {
        _logger.LogDebug( "WebhookDelete" );

        return Ok();
    }


    /// <summary />
    [HttpGet]
    [Route( "webhooks" )]
    public PaginatedResult<Webhook> WebhookList(
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "WebhookList" );

        return new PaginatedResult<Webhook>()
        {
            HasMore = false,
            Data = [
                new Webhook() {
                    Id = Guid.NewGuid(),
                    EndpointUrl = "https://domain.name/sink/",
                    Status = WebhookStatus.Enabled,
                    Events = [ WebhookEventType.EmailDelivered ],
                    SigningSecret = "random-secret-1",
                    MomentCreated = DateTime.UtcNow,
                },

                new Webhook() {
                    Id = Guid.NewGuid(),
                    EndpointUrl = "https://domain.name/sink2/",
                    Status = WebhookStatus.Enabled,
                    Events = [ WebhookEventType.EmailClicked, WebhookEventType.ContactCreated ],
                    SigningSecret = "random-secret-2",
                    MomentCreated = DateTime.UtcNow,
                },

                new Webhook() {
                    Id = Guid.NewGuid(),
                    EndpointUrl = "https://domain.name/sink3/",
                    Status = WebhookStatus.Disabled,
                    Events = [ WebhookEventType.EmailSent ],
                    SigningSecret = "random-secret-3",
                    MomentCreated = DateTime.UtcNow,
                },
            ],
        };
    }
}