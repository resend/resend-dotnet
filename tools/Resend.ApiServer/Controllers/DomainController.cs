using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class DomainController : ControllerBase
{
    private readonly ILogger<DomainController> _logger;


    /// <summary />
    public DomainController( ILogger<DomainController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "domains" )]
    public Domain DomainAdd( [FromBody] DomainAddData request )
    {
        _logger.LogDebug( "DomainAdd" );

        return new Domain()
        {
            Id = Guid.NewGuid(),
            Name = "example.com",
            Region = DeliveryRegion.UsEast1,
            Status = ValidationStatus.NotStarted,
            MomentCreated = DateTime.UtcNow,
            Records = new List<DomainRecord>()
            {
                new DomainRecord()
                {
                    Record = "SPF",
                    RecordType = "TXT",
                    Name = "bounces",
                    TimeToLive = "Auto",
                    Status = ValidationStatus.NotStarted,
                    Value = "feedback-smtp.us-east-1.amazonses.com",
                },
            },
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "domains/{id}" )]
    public Domain DomainRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "DomainRetrieve" );

        return new Domain()
        {
            Id = id,
            Name = "example.com",
            Region = DeliveryRegion.UsEast1,
            Status = ValidationStatus.NotStarted,
            MomentCreated = DateTime.UtcNow,
            Records = new List<DomainRecord>()
            {
                new DomainRecord()
                {
                    Record = "SPF",
                    RecordType = "TXT",
                    Name = "bounces",
                    TimeToLive = "Auto",
                    Status = ValidationStatus.NotStarted,
                    Value = "feedback-smtp.us-east-1.amazonses.com",
                },
            },
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "domains/{id}" )]
    public ActionResult<ObjectId> DomainUpdate( [FromRoute] Guid id, [FromBody] DomainUpdateData data )
    {
        _logger.LogDebug( "DomainUpdate" );

        return Ok( new ObjectId()
        {
            Object = "domain",
            Id = id,
        } );
    }


    /// <summary />
    [HttpPost]
    [Route( "domains/{id}/verify" )]
    public ActionResult<ObjectId> DomainVerify( [FromRoute] Guid id )
    {
        _logger.LogDebug( "DomainVerify" );

        return Ok( new ObjectId()
        {
            Object = "domain",
            Id = id,
        } );
    }


    /// <summary />
    [HttpGet]
    [Route( "domains" )]
    public ListOf<Domain> DomainList()
    {
        _logger.LogDebug( "DomainList" );

        return new ListOf<Domain>()
        {
            Data = new List<Domain>()
            {
                new Domain()
                {
                    Id = Guid.NewGuid(),
                    Name = "example.com",
                    Region = DeliveryRegion.UsEast1,
                    Status = ValidationStatus.NotStarted,
                    MomentCreated = DateTime.UtcNow,
                },
                new Domain()
                {
                    Id = Guid.NewGuid(),
                    Name = "amazing.com",
                    Region = DeliveryRegion.EuWest1,
                    Status = ValidationStatus.Pending,
                    MomentCreated = DateTime.UtcNow,
                }
            },
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "domains/{id}" )]
    public ActionResult DomainDelete( [FromRoute] Guid id )
    {
        _logger.LogDebug( "DomainDelete" );

        return Ok();
    }
}
