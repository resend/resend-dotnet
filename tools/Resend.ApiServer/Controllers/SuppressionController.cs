using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;
using System.Net;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class SuppressionController : ControllerBase
{
    private readonly ILogger<SuppressionController> _logger;


    /// <summary />
    public SuppressionController( ILogger<SuppressionController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "suppressions" )]
    public ObjectId SuppressionAdd( [FromBody] SuppressionAddRequest request )
    {
        _logger.LogDebug( "SuppressionAdd" );

        return new ObjectId()
        {
            Object = "suppression",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "suppressions" )]
    public PaginatedResult<SuppressionSummary> SuppressionList( [FromQuery] string? origin = null )
    {
        _logger.LogDebug( "SuppressionList" );

        var list = new List<SuppressionSummary>();
        list.Add( new SuppressionSummary()
        {
            Id = Guid.NewGuid(),
            Email = "steve.wozniak@gmail.com",
            Origin = origin switch
            {
                "bounce" => SuppressionOrigin.Bounce,
                "complaint" => SuppressionOrigin.Complaint,
                _ => SuppressionOrigin.Manual,
            },
            SourceId = origin == "bounce" ? Guid.NewGuid().ToString() : null,
            MomentCreated = DateTime.UtcNow,
        } );

        return new PaginatedResult<SuppressionSummary>() { HasMore = true, Data = list };
    }


    /// <summary />
    [HttpGet]
    [Route( "suppressions/{suppression}" )]
    public Suppression SuppressionRetrieve( [FromRoute] string suppression )
    {
        _logger.LogDebug( "SuppressionRetrieve" );

        var isId = Guid.TryParse( suppression, out var id );

        return new Suppression()
        {
            Object = "suppression",
            Id = isId == true ? id : Guid.NewGuid(),
            Email = isId == true ? "steve.wozniak@gmail.com" : suppression,
            Origin = SuppressionOrigin.Manual,
            MomentCreated = DateTime.UtcNow,
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "suppressions/{suppression}" )]
    public SuppressionRemoveResult SuppressionRemove( [FromRoute] string suppression )
    {
        _logger.LogDebug( "SuppressionRemove" );

        return new SuppressionRemoveResult()
        {
            Object = "suppression",
            Id = Guid.TryParse( suppression, out var id ) == true ? id : Guid.NewGuid(),
            Deleted = true,
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "suppressions/batch/add" )]
    public ActionResult<ListOf<ObjectId>> SuppressionBatchAdd( [FromBody] SuppressionBatchAddRequest request )
    {
        _logger.LogDebug( "SuppressionBatchAdd" );

        var invalid = ValidateEmails( request.Emails );

        if ( invalid != null )
            return invalid;

        var list = Normalize( request.Emails )
            .Select( _ => new ObjectId() { Object = "suppression", Id = Guid.NewGuid() } )
            .ToList();

        return new ListOf<ObjectId>() { Data = list };
    }


    /// <summary />
    [HttpPost]
    [Route( "suppressions/batch/remove" )]
    public ActionResult<ListOf<SuppressionRemoveResult>> SuppressionBatchRemove( [FromBody] SuppressionBatchRemoveRequest request )
    {
        _logger.LogDebug( "SuppressionBatchRemove" );

        if ( ( request.Emails == null ) == ( request.Ids == null ) )
            return BadRequest();

        if ( request.Emails != null )
        {
            var invalid = ValidateEmails( request.Emails );

            if ( invalid != null )
                return invalid;
        }

        var ids = request.Ids ?? Normalize( request.Emails! ).Select( _ => Guid.NewGuid() ).ToList();

        var list = ids
            .Select( x => new SuppressionRemoveResult() { Object = "suppression", Id = x, Deleted = true } )
            .ToList();

        return new ListOf<SuppressionRemoveResult>() { Data = list };
    }


    /// <summary>
    /// Mirrors the API, which validates every entry as an email address -- a missing or blank
    /// one is rejected outright rather than dropped from the batch.
    /// </summary>
    private ActionResult? ValidateEmails( IEnumerable<string>? emails )
    {
        if ( emails != null && emails.Any( x => string.IsNullOrWhiteSpace( x ) ) == false )
            return null;

        return UnprocessableEntity( new ErrorResponse()
        {
            StatusCode = (int) HttpStatusCode.UnprocessableEntity,
            ErrorType = ErrorType.ValidationError,
            Message = "Each item in `emails` must be a valid email address.",
        } );
    }


    /// <summary>
    /// Mirrors the API, which lowercases, trims and dedupes addresses before writing --
    /// so a batch response can be shorter than the request.
    /// </summary>
    private static List<string> Normalize( IEnumerable<string> emails )
    {
        return emails
            .Select( x => x.ToLowerInvariant().Trim() )
            .Distinct()
            .ToList();
    }
}
