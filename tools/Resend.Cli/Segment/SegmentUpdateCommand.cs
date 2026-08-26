using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Segment;

/// <summary />
[Command( "update", Description = "Update a segment" )]
public class SegmentUpdateCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Segment identifier" )]
    [Required]
    public Guid? SegmentId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Segment name" )]
    [Required]
    public string? Name { get; set; }


    /// <summary />
    public SegmentUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var data = new SegmentData()
        {
            Name = this.Name!,
        };

        await _resend.SegmentUpdateAsync( this.SegmentId!.Value, data );

        return 0;
    }
}
