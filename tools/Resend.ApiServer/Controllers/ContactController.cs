using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;


    /// <summary />
    public ContactController( ILogger<ContactController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "audiences/{audienceId}/contacts" )]
    public ObjectId ContactAdd( [FromRoute] Guid audienceId, [FromBody] ContactData message )
    {
        _logger.LogDebug( "ContactAdd" );

        return new ObjectId()
        {
            Object = "contact",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "audiences/{audienceId}/contacts/{contactId:guid}" )]
    public Contact ContactRetrieve( [FromRoute] Guid audienceId, [FromRoute] Guid contactId )
    {
        _logger.LogDebug( "ContactRetrieve" );

        return new Contact()
        {
            Id = contactId,
            Email = "email@test.com",
            FirstName = "Bob",
            LastName = "Test",
            MomentCreated = DateTime.UtcNow.AddDays( -1 ),
            IsUnsubscribed = true,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "audiences/{audienceId}/contacts/{email}" )]
    public Contact ContactRetrieveByEmail( [FromRoute] Guid audienceId, [FromRoute] string email )
    {
        _logger.LogDebug( "ContactRetrieve" );

        return new Contact()
        {
            Id = Guid.NewGuid(),
            Email = email,
            FirstName = "Bob",
            LastName = "Test",
            MomentCreated = DateTime.UtcNow.AddDays( -1 ),
            IsUnsubscribed = true,
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "audiences/{audienceId}/contacts/{contactId:guid}" )]
    public ObjectId ContactUpdate( [FromRoute] Guid audienceId, [FromRoute] Guid contactId, [FromBody] ContactData message )
    {
        _logger.LogDebug( "ContactUpdate" );

        return new ObjectId()
        {
            Object = "contact",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "audiences/{audienceId}/contacts/{email}" )]
    public ObjectId ContactUpdateByEmail( [FromRoute] Guid audienceId, [FromRoute] string email, [FromBody] ContactData message )
    {
        _logger.LogDebug( "ContactUpdate" );

        return new ObjectId()
        {
            Object = "contact",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "audiences/{audienceId}/contacts/{contactId:guid}" )]
    public ActionResult ContactDelete( [FromRoute] Guid audienceId, Guid contactId )
    {
        _logger.LogDebug( "ContactDelete" );

        return Ok();
    }


    /// <summary />
    [HttpDelete]
    [Route( "audiences/{audienceId}/contacts/{email}" )]
    public ActionResult ContactDeleteByEmail( [FromRoute] Guid audienceId, string email )
    {
        _logger.LogDebug( "ContactDeleteByEmail" );

        return Ok();
    }


    /// <summary />
    [HttpGet]
    [Route( "audiences/{audienceId}/contacts" )]
    public PaginatedResult<Contact> ContactList( [FromRoute] Guid audienceId )
    {
        _logger.LogDebug( "ContactList" );

        var list = new List<Contact>();

        list.Add( new Contact()
        {
            Id = Guid.NewGuid(),
            Email = "test@mail.com",
            FirstName = "Bob",
            LastName = "Test",
            MomentCreated = DateTime.UtcNow,
            IsUnsubscribed = true,
        } );

        list.Add( new Contact()
        {
            Id = Guid.NewGuid(),
            Email = "test2@mail.com",
            FirstName = "Carl",
            LastName = "Test",
            MomentCreated = DateTime.UtcNow,
            IsUnsubscribed = false,
        } );

        return new PaginatedResult<Contact>()
        {
            HasMore = false,
            Data = list,
        };
    }
}
