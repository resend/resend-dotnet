using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class OAuthGrantController : ControllerBase
{
    private readonly ILogger<OAuthGrantController> _logger;


    /// <summary />
    public OAuthGrantController( ILogger<OAuthGrantController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpGet]
    [Route( "oauth/grants" )]
    public PaginatedResult<OAuthGrant> OAuthGrantList()
    {
        _logger.LogDebug( "OAuthGrantList" );

        var list = new List<OAuthGrant>();
        list.Add( new OAuthGrant()
        {
            Id = Guid.NewGuid(),
            ClientId = Guid.NewGuid(),
            Scopes = new List<string>() { "emails:send" },
            MomentCreated = DateTime.UtcNow,
            Client = new OAuthGrantClient() { Name = "Resend CLI" },
        } );

        return new PaginatedResult<OAuthGrant>() { HasMore = false, Data = list };
    }


    /// <summary />
    [HttpDelete]
    [Route( "oauth/grants/{id}" )]
    public OAuthGrantRevoked OAuthGrantRevoke( [FromRoute] Guid id )
    {
        _logger.LogDebug( "OAuthGrantRevoke" );

        return new OAuthGrantRevoked()
        {
            Object = "oauth_grant",
            Id = id,
            MomentRevoked = DateTime.UtcNow,
            RevokedReason = "revoked_from_api",
        };
    }
}
