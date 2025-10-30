using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class TopicController : ControllerBase
{
    private readonly ILogger<TopicController> _logger;


    /// <summary />
    public TopicController( ILogger<TopicController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "topics" )]
    public ObjectId TopicAdd( [FromBody] TopicData request )
    {
        _logger.LogDebug( "TopicAdd" );

        return new ObjectId()
        {
            Object = "topic",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "topics/{id}" )]
    public Topic TopicRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "TopicRetrieve" );

        return new Topic()
        {
            Id = Guid.NewGuid(),
            Name = "Topic",
            Description = "Topic Description",
            SubscriptionDefault = SubscriptionType.OptIn,
            MomentCreated = DateTime.UtcNow,
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "topics/{id}" )]
    public ObjectId TopicUpdate( [FromRoute] Guid id, [FromBody] TopicData data )
    {
        _logger.LogDebug( "TopicUpdate" );

        return new ObjectId()
        {
            Object = "topic",
            Id = id,
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "topics/{id}" )]
    public ActionResult TopicDelete( [FromRoute] Guid id )
    {
        _logger.LogDebug( "TopicDelete" );

        return Ok();
    }


    /// <summary />
    [HttpGet]
    [Route( "topics" )]
    public PaginatedResult<Topic> TopicList(
        [FromQuery] string? limit = null,
        [FromQuery] string? before = null,
        [FromQuery] string? after = null
    )
    {
        _logger.LogDebug( "TopicList" );

        return new PaginatedResult<Topic>()
        {
            HasMore = false,
            Data = [
                new Topic() {
                    Id = Guid.NewGuid(),
                    Name = "Topic 1",
                    Description = "One",
                    SubscriptionDefault = SubscriptionType.OptIn,
                    MomentCreated = DateTime.UtcNow,
                },

                new Topic() {
                    Id = Guid.NewGuid(),
                    Name = "Topic 2",
                    Description = "Two",
                    SubscriptionDefault = SubscriptionType.OptOut,
                    MomentCreated = DateTime.UtcNow,
                },

                new Topic() {
                    Id = Guid.NewGuid(),
                    Name = "Topic 3",
                    Description = "Three",
                    SubscriptionDefault = SubscriptionType.OptIn,
                    MomentCreated = DateTime.UtcNow,
                },
            ],
        };
    }
}