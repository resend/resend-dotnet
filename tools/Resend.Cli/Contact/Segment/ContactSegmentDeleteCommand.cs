using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Contact.Segment;

/// <summary />
[Command( "delete", Description = "Removes a contact from a segment" )]
public class ContactSegmentDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Contact identifier" )]
    [Required]
    public Guid? ContactId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Segment identifier" )]
    [Required]
    public Guid? SegmentId { get; set; }


    /// <summary />
    public ContactSegmentDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.ContactRemoveFromSegmentAsync( this.ContactId!.Value, this.SegmentId!.Value );

        return 0;
    }
}