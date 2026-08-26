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
}
