using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.ContactProp;

/// <summary />
[Command( "delete", Description = "Delete a contact property" )]
public class ContactPropDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Property identifier" )]
    [Required]
    public Guid? PropId { get; set; }


    /// <summary />
    public ContactPropDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.ContactPropDeleteAsync( this.PropId!.Value );

        return 0;
    }
}