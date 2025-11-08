using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Contact;

/// <summary />
[Command( "delete", Description = "Delete a contact" )]
public class ContactDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Contact identifier" )]
    [Required]
    public Guid? ContactId { get; set; }


    /// <summary />
    public ContactDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.ContactDeleteAsync( this.ContactId!.Value );

        return 0;
    }
}
