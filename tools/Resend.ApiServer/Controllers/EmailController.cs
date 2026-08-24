using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;
using System.Net;
using System.Text.Json;

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


    /// <summary>
    /// Fakes the metrics endpoint, echoing back the request's shape (granularity,
    /// requested metrics/dimensions, filters) so tests can assert on how the SDK built the query.
    /// </summary>
    [HttpGet]
    [Route( "emails/metrics" )]
    public ActionResult<EmailMetrics> EmailMetrics(
        [FromQuery( Name = "start_date" )] string? startDate,
        [FromQuery( Name = "end_date" )] string? endDate,
        [FromQuery( Name = "timezone" )] string? timezone,
        [FromQuery( Name = "granularity" )] string? granularity,
        [FromQuery( Name = "metrics" )] string? metrics,
        [FromQuery( Name = "dimensions" )] string? dimensions,
        [FromQuery( Name = "domain_id" )] string? domainId,
        [FromQuery( Name = "email_id" )] string? emailId,
        [FromQuery( Name = "broadcast_id" )] string? broadcastId
    )
    {
        _logger.LogDebug( "EmailMetrics" );

        var end = string.IsNullOrEmpty( endDate ) == false ? DateTime.Parse( endDate ).ToUniversalTime() : DateTime.UtcNow;
        var start = string.IsNullOrEmpty( startDate ) == false ? DateTime.Parse( startDate ).ToUniversalTime() : end.AddDays( -6 );

        var metricNames = SplitCsv( metrics ) ?? AllMetricNames;
        var dimensionNames = SplitCsv( dimensions ) ?? new List<string>();

        var hasEmail = dimensionNames.Contains( "email" ) || string.IsNullOrEmpty( emailId ) == false;
        var hasBroadcast = dimensionNames.Contains( "broadcast" ) || string.IsNullOrEmpty( broadcastId ) == false;

        if ( hasEmail && hasBroadcast )
        {
            return UnprocessableEntity( new ErrorResponse()
            {
                StatusCode = (int) HttpStatusCode.UnprocessableEntity,
                ErrorType = ErrorType.ValidationError,
                Message = "The `broadcast` dimension/`broadcast_id` filter cannot be combined with the `email` dimension/`email_id` filter.",
            } );
        }

        var result = new EmailMetrics()
        {
            Object = "metrics",
            StartDate = start,
            EndDate = end,
            Metrics = metricNames.Select( ParseMetricType ).ToList(),
            Dimensions = dimensionNames.Select( ParseMetricDimension ).ToList(),
            Granularity = granularity switch
            {
                "hourly" => MetricsGranularity.Hourly,
                "weekly" => MetricsGranularity.Weekly,
                "monthly" => MetricsGranularity.Monthly,
                _ => MetricsGranularity.Daily,
            },
            Totals = metricNames.ToDictionary( x => x, x => 10d ),
        };

        if ( dimensionNames.Count > 0 )
        {
            var row = new EmailMetricsDataPoint();

            if ( dimensionNames.Contains( "period" ) == true )
                row.Period = start.ToString( "yyyy-MM-dd" );

            if ( dimensionNames.Contains( "domain" ) == true )
            {
                row.DomainId = SplitCsv( domainId )?.Select( Guid.Parse ).FirstOrDefault() ?? Guid.NewGuid();
                row.DomainName = "example.com";
            }

            if ( dimensionNames.Contains( "email" ) == true )
                row.EmailId = SplitCsv( emailId )?.Select( Guid.Parse ).FirstOrDefault() ?? Guid.NewGuid();

            if ( dimensionNames.Contains( "broadcast" ) == true )
            {
                row.BroadcastId = SplitCsv( broadcastId )?.Select( Guid.Parse ).FirstOrDefault() ?? Guid.NewGuid();
                row.BroadcastName = "July Newsletter";
            }

            foreach ( var name in metricNames )
                row.MetricValues[ name ] = JsonSerializer.SerializeToElement( 10d );

            result.Data = new List<EmailMetricsDataPoint>() { row };
        }

        return result;
    }


    /// <summary />
    private static readonly List<string> AllMetricNames = Enum.GetValues<MetricType>()
        .Select( ParseableName )
        .ToList();


    /// <summary />
    private static List<string>? SplitCsv( string? value )
    {
        if ( string.IsNullOrWhiteSpace( value ) == true )
            return null;

        return value.Split( ',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries ).ToList();
    }


    /// <summary />
    private static string ParseableName( MetricType value )
    {
        return value switch
        {
            MetricType.Received => "received",
            MetricType.Delivered => "delivered",
            MetricType.Complained => "complained",
            MetricType.Suppressed => "suppressed",
            MetricType.Bounced => "bounced",
            MetricType.BouncedTransient => "bounced_transient",
            MetricType.BouncedPermanent => "bounced_permanent",
            MetricType.BouncedUndetermined => "bounced_undetermined",
            MetricType.Opened => "opened",
            MetricType.Clicked => "clicked",
            MetricType.Unsubscribed => "unsubscribed",
            MetricType.DeliveryDelayed => "delivery_delayed",
            MetricType.Failed => "failed",
            MetricType.Sent => "sent",
            MetricType.UniqueOpened => "unique_opened",
            MetricType.UniqueClicked => "unique_clicked",
            MetricType.DeliveryRate => "delivery_rate",
            MetricType.OpenRate => "open_rate",
            MetricType.ClickRate => "click_rate",
            MetricType.BounceRate => "bounce_rate",
            MetricType.ComplaintRate => "complaint_rate",
            MetricType.UnsubscribeRate => "unsubscribe_rate",
            _ => throw new NotImplementedException( $"Unmapped metric type: {value}" ),
        };
    }


    private static readonly HashSet<string> ValidExpiresIn = new( StringComparer.OrdinalIgnoreCase )
    {
        "48h", "10m", "2 hours", "1 day", "1h 30m",
    };


    /// <summary />
    private static MetricType ParseMetricType( string value )
    {
        return value switch
        {
            "received" => MetricType.Received,
            "delivered" => MetricType.Delivered,
            "complained" => MetricType.Complained,
            "suppressed" => MetricType.Suppressed,
            "bounced" => MetricType.Bounced,
            "bounced_transient" => MetricType.BouncedTransient,
            "bounced_permanent" => MetricType.BouncedPermanent,
            "bounced_undetermined" => MetricType.BouncedUndetermined,
            "opened" => MetricType.Opened,
            "clicked" => MetricType.Clicked,
            "unsubscribed" => MetricType.Unsubscribed,
            "delivery_delayed" => MetricType.DeliveryDelayed,
            "failed" => MetricType.Failed,
            "sent" => MetricType.Sent,
            "unique_opened" => MetricType.UniqueOpened,
            "unique_clicked" => MetricType.UniqueClicked,
            "delivery_rate" => MetricType.DeliveryRate,
            "open_rate" => MetricType.OpenRate,
            "click_rate" => MetricType.ClickRate,
            "bounce_rate" => MetricType.BounceRate,
            "complaint_rate" => MetricType.ComplaintRate,
            "unsubscribe_rate" => MetricType.UnsubscribeRate,
            _ => throw new ArgumentOutOfRangeException( nameof( value ), $"Unknown metric: '{value}'" ),
        };
    }


    /// <summary />
    private static MetricDimension ParseMetricDimension( string value )
    {
        return value switch
        {
            "period" => MetricDimension.Period,
            "domain" => MetricDimension.Domain,
            "email" => MetricDimension.Email,
            "broadcast" => MetricDimension.Broadcast,
            _ => throw new ArgumentOutOfRangeException( nameof( value ), $"Unknown dimension: '{value}'" ),
        };
    }
}
