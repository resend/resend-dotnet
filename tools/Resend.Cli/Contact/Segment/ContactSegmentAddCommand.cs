using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Contact.Segment;

/// <summary />
[Command( "add", Description = "Adds contact to a segment" )]
public class ContactSegmentAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Audience identifier" )]
    [Required]
    public Guid? ContactId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Segment identifier" )]
    [Required]
    public Guid? SegmentId { get; set; }


    /// <summary />
    public ContactSegmentAddCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ContactAddToSegmentAsync( this.ContactId!.Value, this.SegmentId!.Value );

        return 0;
    }
}
