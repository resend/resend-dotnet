using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;
using System.Net;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;


    /// <summary />
    public EmailController( ILogger<EmailController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "emails" )]
    public ObjectId EmailSend(
        [FromHeader( Name = "Idempotency-Key" )] string? idempotencyKey,
        [FromBody] EmailMessage message
    )
    {
        _logger.LogDebug( "EmailSend" );

        if ( idempotencyKey != null )
            _logger.LogDebug( "With {IdempotencyKey}", idempotencyKey );

        return new ObjectId()
        {
            Object = "email",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "emails/{id}" )]
    public EmailReceipt EmailRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "EmailRetrieve" );

        return new EmailReceipt()
        {
            Id = id,
            Subject = "Demo",
            From = "onboarding@resend.dev",
            To = "delivered@resend.dev",
            HtmlBody = "This is HTML!",
            MomentCreated = DateTime.UtcNow,
            LastEvent = EmailStatus.Delivered,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "emails" )]
    public PaginatedResult<EmailReceipt> EmailList(
        [FromQuery( Name = "limit" )] int? limit,
        [FromQuery( Name = "after" )] string? after,
        [FromQuery( Name = "before" )] string? before
        )
    {
        _logger.LogDebug( "EmailList" );

        var pr = new PaginatedResult<EmailReceipt>()
        {
            HasMore = true,
            Data = new List<EmailReceipt>(),
        };

        pr.Data.Add( new EmailReceipt()
        {
            Id = Guid.NewGuid(),
            Subject = "Demo #1",
            From = "onboarding@resend.dev",
            To = "delivered@resend.dev",
            HtmlBody = "This is HTML!",
            MomentCreated = DateTime.UtcNow,
            LastEvent = EmailStatus.Delivered,
        } );

        pr.Data.Add( new EmailReceipt()
        {
            Id = Guid.NewGuid(),
            Subject = "Demo #2",
            From = "onboarding@resend.dev",
            To = "delivered@resend.dev",
            HtmlBody = "This is HTML!",
            MomentCreated = DateTime.UtcNow,
            LastEvent = EmailStatus.Delivered,
        } );

        return pr;
    }


    /// <summary />
    [HttpPost]
    [Route( "emails/batch" )]
    public EmailBatchResponse EmailBatch(
        [FromHeader( Name = "Idempotency-Key" )] string? idempotencyKey,
        [FromHeader( Name = "x-batch-validation" )] string? validationMode,
        [FromBody] List<EmailMessage> messages )
    {
        _logger.LogDebug( "EmailBatch" );

        if ( idempotencyKey != null )
            _logger.LogDebug( "With {IdempotencyKey}", idempotencyKey );

        if ( validationMode != null )
            _logger.LogDebug( "With {ValidationMode}", validationMode );


        /*
         *
         */
        var list = new EmailBatchResponse();
        list.Data = messages.Select( x => new EmailBatchReceipt()
        {
            Id = Guid.NewGuid(),
        } ).ToList();

        return list;
    }


    /// <summary />
    [HttpPatch]
    [Route( "emails/{id}" )]
    public ActionResult<ObjectId> EmailReschedule( [FromRoute] Guid id, [FromBody] EmailRescheduleRequest request )
    {
        _logger.LogDebug( "EmailReschedule" );

        if ( request.MomentSchedule.IsMoment == true )
        {
            if ( request.MomentSchedule < DateTime.UtcNow )
            {
                return BadRequest( new ErrorResponse()
                {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    ErrorType = ErrorType.ApplicationError,
                    Message = "Moment in past",
                } );
            }
        }

        if ( request.MomentSchedule.IsMoment == false )
            _logger.LogInformation( "Resend will reschedule for: {MomentSchedule}", request.MomentSchedule.Human );

        return new ObjectId()
        {
            Object = "email",
            Id = id,
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "emails/{id}/cancel" )]
    public ObjectId EmailCancel( [FromRoute] Guid id )
    {
        _logger.LogDebug( "EmailCancel" );

        return new ObjectId()
        {
            Object = "email",
            Id = id,
        };
    }


    /// <summary>
    /// The fake server has no real duration parser -- it isn't the API, so it shouldn't
    /// try to replicate the API's validation grammar. It only needs to return realistic
    /// responses for the fixed set of inputs the test suite exercises. The fake server
    /// also has no persisted state, so the well-known empty id doubles as the
    /// "email not found" fixture for tests.
    /// </summary>
    [HttpPost]
    [Route( "emails/{id}/share" )]
    public ActionResult<EmailShareResult> EmailShare( [FromRoute] Guid id, [FromBody] EmailShareRequest? request )
    {
        _logger.LogDebug( "EmailShare" );

        if ( id == Guid.Empty )
        {
            return NotFound( new ErrorResponse()
            {
                StatusCode = (int) HttpStatusCode.NotFound,
                ErrorType = ErrorType.NotFound,
                Message = "Email not found",
            } );
        }

        var expiresIn = request?.ExpiresIn ?? "48h";

        if ( ValidExpiresIn.Contains( expiresIn ) == false )
        {
            return UnprocessableEntity( new ErrorResponse()
            {
                StatusCode = (int) HttpStatusCode.UnprocessableEntity,
                ErrorType = ErrorType.ValidationError,
                Message = "`expires_in` must be a valid duration, capped at 48 hours.",
            } );
        }

        return new EmailShareResult()
        {
            Object = "email",
            Id = id,
            Url = $"https://resend.com/share/{id}",
        };
    }


    private static readonly HashSet<string> ValidExpiresIn = new( StringComparer.OrdinalIgnoreCase )
    {
        "48h", "10m", "2 hours", "1 day", "1h 30m",
    };
}
