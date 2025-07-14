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
    [HttpPost]
    [Route( "emails/batch" )]
    public ListOf<ObjectId> EmailBatch(
        [FromHeader( Name = "Idempotency-Key" )] string? idempotencyKey,
        [FromBody] List<EmailMessage> messages )
    {
        _logger.LogDebug( "EmailBatch" );

        if ( idempotencyKey != null )
            _logger.LogDebug( "With {IdempotencyKey}", idempotencyKey );

        var list = new ListOf<ObjectId>();
        list.Data = messages.Select( x => new ObjectId()
        {
            Object = "email",
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
}
