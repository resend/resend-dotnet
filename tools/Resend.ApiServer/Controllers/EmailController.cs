using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;
using System.Net;
using System.Text.RegularExpressions;

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
    /// Mirrors the API: <c>expires_in</c> defaults to 48h, is validated as a human-readable
    /// duration and is capped at 48h. The fake server has no persisted state, so the
    /// well-known empty id doubles as the "email not found" fixture for tests.
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

        if ( TryParseExpiresIn( expiresIn, out var duration ) == false || duration > TimeSpan.FromHours( 48 ) )
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


    private static readonly Regex DurationTokenPattern = new Regex( @"(\d+)\s*([a-zA-Z]+)", RegexOptions.Compiled );


    /// <summary>
    /// Parses a human-readable duration such as <c>"10m"</c>, <c>"2 hours"</c>, <c>"1 day"</c>
    /// or <c>"1h 30m"</c> into a <see cref="TimeSpan"/>.
    /// </summary>
    private static bool TryParseExpiresIn( string value, out TimeSpan duration )
    {
        duration = TimeSpan.Zero;

        var trimmed = value.Trim();

        if ( trimmed.Length == 0 )
            return false;

        var matches = DurationTokenPattern.Matches( trimmed );

        if ( matches.Count == 0 )
            return false;

        /*
         * Reject stray characters that aren't part of a "<number><unit>" token or
         * whitespace between tokens -- e.g. "banana" or "10x". Gaps before/between/after
         * matches (such as the space in "1h 30m") must be whitespace-only.
         */
        var cursor = 0;

        foreach ( Match m in matches )
        {
            var gap = trimmed[ cursor..m.Index ];

            if ( gap.Any( c => char.IsWhiteSpace( c ) == false ) )
                return false;

            cursor = m.Index + m.Length;
        }

        if ( trimmed[ cursor.. ].Any( c => char.IsWhiteSpace( c ) == false ) )
            return false;

        foreach ( Match m in matches )
        {
            if ( long.TryParse( m.Groups[ 1 ].Value, out var amount ) == false )
                return false;

            var unit = m.Groups[ 2 ].Value.ToLowerInvariant();

            TimeSpan? token = unit switch
            {
                "h" or "hr" or "hrs" or "hour" or "hours" => amount <= 48 ? TimeSpan.FromHours( amount ) : null,
                "m" or "min" or "mins" or "minute" or "minutes" => amount <= 2880 ? TimeSpan.FromMinutes( amount ) : null,
                "d" or "day" or "days" => amount <= 2 ? TimeSpan.FromDays( amount ) : null,
                _ => null,
            };

            if ( token == null || duration > TimeSpan.FromHours( 48 ) - token.Value )
                return false;

            duration += token.Value;
        }

        return true;
    }
}
