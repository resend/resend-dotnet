using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Broadcast;

/// <summary />
[Command( "duplicate", Description = "Duplicate a broadcast" )]
public class BroadcastDuplicateCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Broadcast identifier" )]
    [Required]
    public Guid? BroadcastId { get; set; }


    /// <summary />
    public BroadcastDuplicateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.BroadcastDuplicateAsync( this.BroadcastId!.Value );
        Console.WriteLine( res.Content );

        return 0;
    }
}
