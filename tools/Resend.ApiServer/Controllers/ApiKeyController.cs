using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class ApiKeyController : ControllerBase
{
    private readonly ILogger<ApiKeyController> _logger;


    /// <summary />
    public ApiKeyController( ILogger<ApiKeyController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "api-keys" )]
    public ApiKeyData ApiKeyCreate( [FromBody] ApiKeyCreateRequest message )
    {
        _logger.LogDebug( "ApiKeyCreate" );

        return new ApiKeyData()
        {
            Id = Guid.NewGuid(),
            Token = "re_" + Guid.NewGuid().ToString(),
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "api-keys/{id}" )]
    public ObjectId ApiKeyUpdate( [FromRoute] Guid id, [FromBody] ApiKeyUpdateRequest request )
    {
        _logger.LogDebug( "ApiKeyUpdate" );

        return new ObjectId()
        {
            Object = "api_key",
            Id = id,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "api-keys" )]
    public ListOf<ApiKey> ApiKeyList()
    {
        _logger.LogDebug( "ApiKeyList" );

        var list = new List<ApiKey>();

        list.Add( new ApiKey()
        {
            Id = Guid.NewGuid(),
            Name = "Domain #1",
            MomentCreated = DateTime.UtcNow,
        } );

        list.Add( new ApiKey()
        {
            Id = Guid.NewGuid(),
            Name = "Domain #2",
            MomentCreated = DateTime.UtcNow,
        } );

        return new ListOf<ApiKey>()
        {
            Data = list,
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "api-keys/{id}" )]
    public ActionResult ApiKeyDelete( [FromRoute] Guid id )
    {
        return Ok();
    }
}
