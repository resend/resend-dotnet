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


    /// <summary />
    [HttpGet]
    [Route( "webhooks/{webhookId}/events" )]
    public WebhookEventListResult WebhookEventList(
        [FromRoute] Guid webhookId,
        [FromQuery] string? limit = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "WebhookEventList" );

        return new WebhookEventListResult()
        {
            Object = "list",
            HasMore = limit == "1" && after == "msg_1srOrx2ZWZBpBUvZwXKQmoEYga2",
            Data = [
                new WebhookEventLog()
                {
                    Id = "msg_2aQqFEiKYaC8Q35b3e97qyRmaN7",
                    Type = WebhookEventType.EmailSent,
                    MomentCreated = DateTime.UtcNow,
                    Status = WebhookEventLogStatus.Success,
                },
            ],
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "webhooks/{webhookId}/events/{eventId}" )]
    public WebhookEventDetails WebhookEventRetrieve( [FromRoute] Guid webhookId, [FromRoute] string eventId )
    {
        _logger.LogDebug( "WebhookEventRetrieve" );

        return new WebhookEventDetails()
        {
            Object = "webhook_event",
            Id = eventId,
            Type = WebhookEventType.EmailSent,
            MomentCreated = DateTime.UtcNow,
            Status = WebhookEventLogStatus.Failed,
            MomentNextAttempt = null,
            Payload = System.Text.Json.JsonSerializer.SerializeToElement( new
            {
                type = "email.sent",
                data = new { email_id = "email_123" },
            } ),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "webhooks/{webhookId}/events/{eventId}/attempts" )]
    public WebhookEventAttemptListResult WebhookEventAttemptList(
        [FromRoute] Guid webhookId,
        [FromRoute] string eventId,
        [FromQuery] string? limit = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "WebhookEventAttemptList" );

        return new WebhookEventAttemptListResult()
        {
            Object = "list",
            HasMore = limit == "1" && after == "atmpt_2ZbUCwvGmIT4mLIN6d3Yz0Ainbd",
            Data = [
                new WebhookEventAttempt()
                {
                    Id = "atmpt_3ZbUCwvGmIT4mLIN6d3Yz0Ainbe",
                    HttpStatusCode = 200,
                    Response = "{\"ok\":true}",
                    MomentSent = DateTime.UtcNow,
                },
            ],
        };
    }
}
