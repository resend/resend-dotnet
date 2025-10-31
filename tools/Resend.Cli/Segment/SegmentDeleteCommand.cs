using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Segment;

/// <summary />
[Command( "delete", Description = "Delete a segment" )]
public class SegmentDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Segment identifier" )]
    [Required]
    public Guid? SegmentId { get; set; }


    /// <summary />
    public SegmentDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.SegmentDeleteAsync( this.SegmentId!.Value );

        return 0;
    }
}