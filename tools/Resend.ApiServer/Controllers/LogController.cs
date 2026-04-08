using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class LogController : ControllerBase
{
    private readonly ILogger<LogController> _logger;


    /// <summary />
    public LogController( ILogger<LogController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpGet]
    [Route( "logs" )]
    public PaginatedResult<Log> LogList(
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "LogList" );

        var id1 = Guid.Parse( "37e4414c-5e25-4dbc-a071-43552a4bd53b" );
        var id2 = Guid.Parse( "a1b2c3d4-5e6f-7a8b-9c0d-1e2f3a4b5c6d" );

        return new PaginatedResult<Log>()
        {
            HasMore = false,
            Data =
            [
                new Log()
                {
                    Id = id1,
                    MomentCreated = DateTime.Parse( "2026-03-30 13:43:54.622865+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    Endpoint = "/emails",
                    HttpMethod = "POST",
                    ResponseStatus = 200,
                    UserAgent = "resend-node:6.0.3",
                },
                new Log()
                {
                    Id = id2,
                    MomentCreated = DateTime.Parse( "2026-03-30 12:15:00.123456+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    Endpoint = "/emails/4ef9a417-02e9-4d39-ad75-9611e0fcc33c",
                    HttpMethod = "GET",
                    ResponseStatus = 200,
                    UserAgent = "curl/8.7.1",
                },
            ],
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "logs/{id}" )]
    public Log LogRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "LogRetrieve" );

        var responseBodyJson = $"{{\"id\":\"{id}\"}}";

        return new Log()
        {
            Id = id,
            MomentCreated = DateTime.UtcNow,
            Endpoint = "/emails",
            HttpMethod = "POST",
            ResponseStatus = 200,
            UserAgent = "resend-node:6.0.3",
            RequestBody = JsonDocument.Parse( "{\"from\":\"sender@example.com\",\"to\":[\"recipient@example.com\"],\"subject\":\"Test subject\"}" ).RootElement,
            ResponseBody = JsonDocument.Parse( responseBodyJson ).RootElement,
        };
    }
}
