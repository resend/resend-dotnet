using Microsoft.AspNetCore.Mvc;
using Resend;
using Resend.Payloads;
using System.Text.Json;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class AutomationController : ControllerBase
{
    private readonly ILogger<AutomationController> _logger;


    /// <summary />
    public AutomationController( ILogger<AutomationController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "automations" )]
    public ObjectId AutomationCreate( [FromBody] AutomationCreateData request )
    {
        _logger.LogDebug( "AutomationCreate" );

        return new ObjectId()
        {
            Object = "automation",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "automations/{id}" )]
    public ObjectId AutomationUpdate( [FromRoute] Guid id, [FromBody] AutomationUpdateData request )
    {
        _logger.LogDebug( "AutomationUpdate" );

        return new ObjectId()
        {
            Object = "automation",
            Id = id,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "automations/{id}" )]
    public Automation AutomationRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "AutomationRetrieve" );

        var stepA = Guid.Parse( "a1b2c3d4-e5f6-7890-abcd-ef1234567890" );
        var stepB = Guid.Parse( "b2c3d4e5-f6a7-8901-bcde-f12345678901" );

        return new Automation()
        {
            Object = "automation",
            Id = id,
            Name = "Welcome series",
            Status = "enabled",
            MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
            MomentUpdated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
            Steps =
            [
                new AutomationStep()
                {
                    Type = "trigger",
                    Config = JsonDocument.Parse( "{\"event_name\":\"user.created\"}" ).RootElement,
                },
                new AutomationStep()
                {
                    Type = "send_email",
                    Config = JsonDocument.Parse( "{\"template_id\":\"tpl_xxxxxxxxx\",\"subject\":\"Welcome!\",\"from\":\"Acme <hello@example.com>\"}" ).RootElement,
                },
            ],
            Edges =
            [
                new AutomationEdge()
                {
                    From = stepA.ToString(),
                    To = stepB.ToString(),
                    EdgeType = "default",
                },
            ],
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "automations" )]
    public PaginatedResult<AutomationSummary> AutomationList(
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null,
        [FromQuery] string? status = null
    )
    {
        _logger.LogDebug( "AutomationList" );

        var id1 = Guid.Parse( "c9b16d4f-ba6c-4e2e-b044-6bf4404e57fd" );
        var id2 = Guid.Parse( "662892b2-4270-4130-a186-33a19752319d" );

        return new PaginatedResult<AutomationSummary>()
        {
            HasMore = false,
            Data =
            [
                new AutomationSummary()
                {
                    Id = id1,
                    Name = "Welcome series",
                    Status = "enabled",
                    MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentUpdated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
                new AutomationSummary()
                {
                    Id = id2,
                    Name = "Re-engagement",
                    Status = "disabled",
                    MomentCreated = DateTime.Parse( "2025-09-15 08:30:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentUpdated = DateTime.Parse( "2025-09-20 14:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
            ],
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "automations/{id}/stop" )]
    public AutomationStopResult AutomationStop( [FromRoute] Guid id )
    {
        _logger.LogDebug( "AutomationStop" );

        return new AutomationStopResult()
        {
            Object = "automation",
            Id = id,
            Status = "disabled",
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "automations/{id}" )]
    public AutomationDeleteResult AutomationDelete( [FromRoute] Guid id )
    {
        _logger.LogDebug( "AutomationDelete" );

        return new AutomationDeleteResult()
        {
            Object = "automation",
            Id = id,
            Deleted = true,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "automations/{automationId}/runs" )]
    public PaginatedResult<AutomationRunSummary> AutomationRunList(
        [FromRoute] Guid automationId,
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null,
        [FromQuery] string? status = null
    )
    {
        _logger.LogDebug( "AutomationRunList" );

        var run1 = Guid.Parse( "a1b2c3d4-e5f6-7890-abcd-ef1234567890" );
        var run2 = Guid.Parse( "b2c3d4e5-f6a7-8901-bcde-f12345678901" );

        return new PaginatedResult<AutomationRunSummary>()
        {
            HasMore = false,
            Data =
            [
                new AutomationRunSummary()
                {
                    Id = run1,
                    Status = "completed",
                    MomentStarted = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentCompleted = DateTime.Parse( "2025-10-01 12:05:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
                new AutomationRunSummary()
                {
                    Id = run2,
                    Status = "running",
                    MomentStarted = DateTime.Parse( "2025-10-02 08:30:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentCompleted = null,
                    MomentCreated = DateTime.Parse( "2025-10-02 08:30:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
            ],
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "automations/{automationId}/runs/{runId}" )]
    public AutomationRun AutomationRunRetrieve( [FromRoute] Guid automationId, [FromRoute] Guid runId )
    {
        _logger.LogDebug( "AutomationRunRetrieve" );

        return new AutomationRun()
        {
            Object = "automation_run",
            Id = runId,
            Status = "completed",
            MomentStarted = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
            MomentCompleted = DateTime.Parse( "2025-10-01 12:05:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
            MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
            Steps =
            [
                new AutomationRunStep()
                {
                    Type = "trigger",
                    Status = "completed",
                    MomentStarted = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentCompleted = DateTime.Parse( "2025-10-01 12:00:01.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    Output = null,
                    Error = null,
                    MomentCreated = DateTime.Parse( "2025-10-01 12:00:00.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
                new AutomationRunStep()
                {
                    Type = "send_email",
                    Status = "completed",
                    MomentStarted = DateTime.Parse( "2025-10-01 12:00:01.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    MomentCompleted = DateTime.Parse( "2025-10-01 12:00:02.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                    Output = null,
                    Error = null,
                    MomentCreated = DateTime.Parse( "2025-10-01 12:00:01.000000+00", null, System.Globalization.DateTimeStyles.RoundtripKind ),
                },
            ],
        };
    }
}
