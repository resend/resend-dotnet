using Microsoft.AspNetCore.Mvc;
using Resend;
using Resend.Payloads;
using System.Text.Json;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class EventController : ControllerBase
{
    private readonly ILogger<EventController> _logger;


    /// <summary />
    public EventController( ILogger<EventController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "events" )]
    [ProducesResponseType( typeof( ObjectId ), StatusCodes.Status201Created )]
    public IActionResult EventCreate( [FromBody] EventCreateData request )
    {
        _logger.LogDebug( "EventCreate" );

        return StatusCode( StatusCodes.Status201Created, new ObjectId()
        {
            Object = "event",
            Id = Guid.NewGuid(),
        } );
    }


    /// <summary />
    [HttpGet]
    [Route( "events/{id}" )]
    public EventResource EventRetrieve( [FromRoute] string id )
    {
        _logger.LogDebug( "EventRetrieve" );

        var isGuid = Guid.TryParse( id, out var eid );

        return new EventResource()
        {
            Object = "event",
            Id = isGuid ? eid : Guid.Parse( "a1b2c3d4-e5f6-7890-abcd-ef1234567890" ),
            Name = isGuid ? "user.created" : id,
            Schema = JsonDocument.Parse( "{\"plan\":\"string\"}" ).RootElement,
            MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
            MomentUpdated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "events" )]
    public PaginatedResult<EventResource> EventList(
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "EventList" );

        var id1 = Guid.Parse( "a1b2c3d4-e5f6-7890-abcd-ef1234567890" );
        var id2 = Guid.Parse( "b2c3d4e5-f6a7-8901-bcde-f12345678901" );

        return new PaginatedResult<EventResource>()
        {
            HasMore = false,
            Data =
            [
                new EventResource()
                {
                    Object = "event",
                    Id = id1,
                    Name = "user.created",
                    Schema = JsonDocument.Parse( "{\"plan\":\"string\"}" ).RootElement,
                    MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentUpdated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
                new EventResource()
                {
                    Object = "event",
                    Id = id2,
                    Name = "user.upgraded",
                    Schema = null,
                    MomentCreated = DateTime.Parse( "2025-09-15 08:30:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentUpdated = DateTime.Parse( "2025-09-20 14:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
            ],
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "events/{id}" )]
    public IActionResult EventUpdate( [FromRoute] string id, [FromBody] EventUpdateData request )
    {
        _logger.LogDebug( "EventUpdate" );

        return Ok();
    }


    /// <summary />
    [HttpDelete]
    [Route( "events/{id}" )]
    public IActionResult EventDelete( [FromRoute] string id )
    {
        _logger.LogDebug( "EventDelete" );

        return Ok();
    }


    /// <summary />
    [HttpPost]
    [Route( "events/send" )]
    [ProducesResponseType( typeof( EventSendResult ), StatusCodes.Status202Accepted )]
    public IActionResult EventSend( [FromBody] EventSendData request )
    {
        _logger.LogDebug( "EventSend" );

        return StatusCode( StatusCodes.Status202Accepted, new EventSendResult()
        {
            Object = "event",
            Event = request.Event,
        } );
    }
}
