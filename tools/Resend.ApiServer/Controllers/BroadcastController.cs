using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;
using System.Net;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class BroadcastController : ControllerBase
{
    private readonly ILogger<BroadcastController> _logger;


    /// <summary />
    public BroadcastController( ILogger<BroadcastController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "broadcasts" )]
    public ObjectId BroadcastAdd( [FromBody] BroadcastData message )
    {
        _logger.LogDebug( "BroadcastAdd" );

        return new ObjectId()
        {
            Object = "broadcast",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "broadcasts/{broadcastId}" )]
    public Broadcast BroadcastRetrieve( Guid broadcastId )
    {
        _logger.LogDebug( "BroadcastRetrieve" );

        return new Broadcast()
        {
            Id = broadcastId,
            SegmentId = Guid.NewGuid(),
            DisplayName = "Display Name",
            Status = BroadcastStatus.Draft,
            MomentCreated = DateTime.UtcNow,
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "broadcasts/{broadcastId}" )]
    public ObjectId BroadcastUpdate( [FromRoute] Guid broadcastId, [FromBody] BroadcastUpdateData message )
    {
        _logger.LogDebug( "BroadcastUpdate" );

        return new ObjectId()
        {
            Object = "broadcast",
            Id = broadcastId,
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "broadcasts/{broadcastId}/send" )]
    public ActionResult BroadcastSend( [FromRoute] Guid broadcastId, [FromBody] BroadcastScheduleRequest message )
    {
        if ( message.MomentSchedule == null )
            _logger.LogDebug( "BroadcastSend" );
        else
            _logger.LogDebug( "BroadcastSchedule" );

        return Ok();
    }


    /// <summary />
    [HttpPost]
    [Route( "broadcasts/{broadcastId}/cancel" )]
    public ActionResult BroadcastCancel( [FromRoute] Guid broadcastId )
    {
        _logger.LogDebug( "BroadcastCancel" );

        return Ok();
    }


    /// <summary>
    /// The fake server has no persisted state, so the well-known empty id doubles as the
    /// "broadcast not found" fixture for tests, mirroring <see cref="EmailController.EmailShare"/>.
    /// </summary>
    [HttpGet]
    [Route( "broadcasts/{broadcastId}/recipients" )]
    public ActionResult<PaginatedResult<BroadcastRecipient>> BroadcastListRecipients(
        [FromRoute] Guid broadcastId,
        [FromQuery( Name = "type" )] string type,
        [FromQuery( Name = "email" )] string? email,
        [FromQuery( Name = "bounce_type" )] string? bounceType,
        [FromQuery( Name = "limit" )] int? limit,
        [FromQuery( Name = "after" )] string? after,
        [FromQuery( Name = "before" )] string? before )
    {
        _logger.LogDebug( "BroadcastListRecipients" );

        if ( broadcastId == Guid.Empty )
        {
            return NotFound( new ErrorResponse()
            {
                StatusCode = (int) HttpStatusCode.NotFound,
                ErrorType = ErrorType.NotFound,
                Message = "Broadcast not found",
            } );
        }

        var recipient = new BroadcastRecipient()
        {
            Id = "b2Zmc2V0OjA",
            ContactId = Guid.NewGuid(),
            Email = email ?? "steve.wozniak@gmail.com",
        };

        switch ( type )
        {
            case "opened":
                recipient.Count = 3;
                break;

            case "clicked":
                recipient.Count = 3;
                recipient.ClickedLinks = new List<BroadcastRecipientClickedLink>()
                {
                    new BroadcastRecipientClickedLink() { Url = "https://resend.com/pricing", Clicks = 2 },
                };
                break;

            case "bounced":
                recipient.BounceType = bounceType switch
                {
                    "transient" => BroadcastRecipientBounceType.Transient,
                    "undetermined" => BroadcastRecipientBounceType.Undetermined,
                    _ => BroadcastRecipientBounceType.Permanent,
                };
                break;
        }

        return new PaginatedResult<BroadcastRecipient>()
        {
            HasMore = true,
            Data = new List<BroadcastRecipient>() { recipient },
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "broadcasts/{broadcastId}" )]
    public ActionResult BroadcastDelete( [FromRoute] Guid broadcastId )
    {
        _logger.LogDebug( "BroadcastDelete" );

        return Ok();
    }


    /// <summary />
    [HttpGet]
    [Route( "broadcasts" )]
    public ListOf<Broadcast> BroadcastList()
    {
        _logger.LogDebug( "BroadcastList" );

        var list = new List<Broadcast>();

        list.Add( new Broadcast()
        {
            Id = Guid.NewGuid(),
            SegmentId = Guid.NewGuid(),
            DisplayName = "In draft",
            Status = BroadcastStatus.Draft,
            MomentCreated = DateTime.UtcNow,
        } );

        list.Add( new Broadcast()
        {
            Id = Guid.NewGuid(),
            SegmentId = Guid.NewGuid(),
            DisplayName = "Scheduled",
            Status = BroadcastStatus.Draft,
            MomentCreated = DateTime.UtcNow,
            MomentScheduled = DateTime.UtcNow.AddDays( 5 ),
        } );

        list.Add( new Broadcast()
        {
            Id = Guid.NewGuid(),
            SegmentId = Guid.NewGuid(),
            DisplayName = "Sent",
            Status = BroadcastStatus.Sent,
            MomentCreated = DateTime.UtcNow.AddDays( -10 ),
            MomentSent = DateTime.UtcNow,
        } );

        return new ListOf<Broadcast>()
        {
            Data = list,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "broadcasts/{broadcastId}/clicked-links" )]
    public PaginatedResult<BroadcastClickedLink> BroadcastClickedLinks(
        [FromRoute] Guid broadcastId,
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "BroadcastClickedLinks" );

        return new PaginatedResult<BroadcastClickedLink>()
        {
            HasMore = false,
            Data =
            [
                new BroadcastClickedLink()
                {
                    Id = "b2Zmc2V0OjA",
                    Url = "https://resend.com/pricing",
                    Clicks = 42,
                    UniqueClicks = 30,
                },
                new BroadcastClickedLink()
                {
                    Id = "b2Zmc2V0OjE",
                    Url = "https://resend.com/docs",
                    Clicks = 17,
                    UniqueClicks = 15,
                },
            ],
        };
    }
}
