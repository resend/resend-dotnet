using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class UsageController : ControllerBase
{
    private readonly ILogger<UsageController> _logger;


    /// <summary />
    public UsageController( ILogger<UsageController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpGet]
    [Route( "usage" )]
    public Usage UsageRetrieve()
    {
        _logger.LogDebug( "UsageRetrieve" );

        return new Usage()
        {
            Object = "usage",
            Emails = new UsageEmails()
            {
                Daily = new UsageEmailDaily()
                {
                    Used = 258,
                    Limit = null,
                    Sent = 57,
                    Received = 201,
                    MomentReset = ParseMoment( "2026-07-17T00:00:00.000Z" ),
                },
                Monthly = new UsageEmailMonthly()
                {
                    Used = 5422,
                    Limit = 10000,
                    Sent = 1000,
                    Received = 4442,
                    MomentReset = ParseMoment( "2026-08-01T00:00:00.000Z" ),
                },
            },
            Contacts = new UsageContacts()
            {
                Used = 85000,
                Limit = 150000,
            },
            Segments = new UsageSegments()
            {
                Used = 2,
                Limit = 3,
            },
            Broadcasts = new UsageBroadcasts()
            {
                Used = 100,
                Limit = null,
            },
            AiCredits = new UsageAiCredits()
            {
                Used = 0,
                Limit = 500,
                MomentNextIncrease = ParseMoment( "2026-07-18T09:00:00.000Z" ),
            },
            AutomationRuns = new UsageAutomationRuns()
            {
                Used = 0,
                Limit = 1000,
                MomentReset = ParseMoment( "2026-08-01T00:00:00.000Z" ),
            },
            Domains = new UsageDomains()
            {
                Used = 1,
                Limit = 1000,
            },
            RateLimit = new UsageRateLimit()
            {
                Limit = 10,
                Duration = "1000ms",
            },
        };
    }


    private static DateTime ParseMoment( string value )
    {
        return DateTime.Parse( value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind );
    }
}
