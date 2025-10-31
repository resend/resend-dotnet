using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Segment;

/// <summary />
[Command( "add", Description = "Create a segment" )]
public class SegmentAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Segment name" )]
    [Required]
    public string? Name { get; set; }


    /// <summary />
    public SegmentAddCommand( IResend resend )
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

        var res = await _resend.SegmentCreateAsync( data );
        var segmentId = res.Content;


        /*
         * 
         */
        Console.WriteLine( segmentId );

        return 0;
    }
}