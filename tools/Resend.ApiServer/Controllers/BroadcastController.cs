using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

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
            AudienceId = Guid.NewGuid(),
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
            AudienceId = Guid.NewGuid(),
            DisplayName = "In draft",
            Status = BroadcastStatus.Draft,
            MomentCreated = DateTime.UtcNow,
        } );

        list.Add( new Broadcast()
        {
            Id = Guid.NewGuid(),
            AudienceId = Guid.NewGuid(),
            DisplayName = "Scheduled",
            Status = BroadcastStatus.Draft,
            MomentCreated = DateTime.UtcNow,
            MomentScheduled = DateTime.UtcNow.AddDays( 5 ),
        } );

        list.Add( new Broadcast()
        {
            Id = Guid.NewGuid(),
            AudienceId = Guid.NewGuid(),
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
}
