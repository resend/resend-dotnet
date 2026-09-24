using Microsoft.AspNetCore.Mvc;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class SegmentController : ControllerBase
{
    private readonly ILogger<SegmentController> _logger;


    /// <summary />
    public SegmentController( ILogger<SegmentController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPatch]
    [Route( "segments/{id}" )]
    public SegmentUpdateResult SegmentUpdate( [FromRoute] Guid id, [FromBody] SegmentData data )
    {
        _logger.LogDebug( "SegmentUpdate" );

        return new SegmentUpdateResult()
        {
            Object = "segment",
            Id = id,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "segments/{id}/contacts" )]
    public PaginatedResult<Contact> SegmentListContacts( [FromRoute] Guid id )
    {
        _logger.LogDebug( "SegmentListContacts" );

        var list = new List<Contact>();

        list.Add( new Contact()
        {
            Id = Guid.NewGuid(),
            Email = "test@mail.com",
            FirstName = "Bob",
            LastName = "Test",
            MomentCreated = DateTime.UtcNow,
            IsUnsubscribed = false,
        } );

        return new PaginatedResult<Contact>()
        {
            HasMore = false,
            Data = list,
        };
    }
}
