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
            Status = DomainStatus.NotStarted,
            MomentCreated = DateTime.UtcNow,
            Records = new List<DomainRecord>()
            {
                new DomainRecord()
                {
                    Record = "SPF",
                    RecordType = "TXT",
                    Name = "bounces",
                    TimeToLive = "Auto",
                    Status = DomainRecordStatus.NotStarted,
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
            Status = DomainStatus.NotStarted,
            MomentCreated = DateTime.UtcNow,
            Records = new List<DomainRecord>()
            {
                new DomainRecord()
                {
                    Record = "SPF",
                    RecordType = "TXT",
                    Name = "bounces",
                    TimeToLive = "Auto",
                    Status = DomainRecordStatus.NotStarted,
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
                    Status = DomainStatus.NotStarted,
                    MomentCreated = DateTime.UtcNow,
                },
                new Domain()
                {
                    Id = Guid.NewGuid(),
                    Name = "amazing.com",
                    Region = DeliveryRegion.EuWest1,
                    Status = DomainStatus.Pending,
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


    /// <summary />
    [HttpPost]
    [Route( "domains/claim" )]
    public DomainClaim DomainClaim( [FromBody] DomainClaimData request )
    {
        _logger.LogDebug( "DomainClaim" );

        return new DomainClaim()
        {
            Object = "domain_claim",
            Id = Guid.NewGuid(),
            Name = request.DomainName,
            Status = DomainClaimStatus.Pending,
            DomainId = Guid.NewGuid(),
            Region = request.Region ?? DeliveryRegion.UsEast1,
            Record = new DomainClaimRecord()
            {
                RecordType = "TXT",
                Name = request.DomainName,
                Value = "resend-domain-verification=3f8a1c2d4e5b6a7f8091a2b3c4d5e6f7",
                TimeToLive = "Auto",
            },
            MomentCreated = DateTime.UtcNow,
            MomentExpires = DateTime.UtcNow.AddDays( 7 ),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "domains/{id}/claim" )]
    public DomainClaim DomainClaimRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "DomainClaimRetrieve" );

        return new DomainClaim()
        {
            Object = "domain_claim",
            Id = Guid.NewGuid(),
            Name = "example.com",
            Status = DomainClaimStatus.Pending,
            DomainId = id,
            Region = DeliveryRegion.UsEast1,
            Record = new DomainClaimRecord()
            {
                RecordType = "TXT",
                Name = "example.com",
                Value = "resend-domain-verification=3f8a1c2d4e5b6a7f8091a2b3c4d5e6f7",
                TimeToLive = "Auto",
            },
            MomentCreated = DateTime.UtcNow,
            MomentExpires = DateTime.UtcNow.AddDays( 7 ),
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "domains/{id}/claim/verify" )]
    public DomainClaim DomainClaimVerify( [FromRoute] Guid id )
    {
        _logger.LogDebug( "DomainClaimVerify" );

        return new DomainClaim()
        {
            Object = "domain_claim",
            Id = Guid.NewGuid(),
            Name = "example.com",
            Status = DomainClaimStatus.Pending,
            DomainId = id,
            Region = DeliveryRegion.UsEast1,
            Record = new DomainClaimRecord()
            {
                RecordType = "TXT",
                Name = "example.com",
                Value = "resend-domain-verification=3f8a1c2d4e5b6a7f8091a2b3c4d5e6f7",
                TimeToLive = "Auto",
            },
            MomentCreated = DateTime.UtcNow,
            MomentExpires = DateTime.UtcNow.AddDays( 7 ),
        };
    }
}
