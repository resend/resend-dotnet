using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Broadcast;

/// <summary />
[Command( "cancel", Description = "Cancels a queued or scheduled broadcast" )]
public class BroadcastCancelCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Broadcast identifier" )]
    [Required]
    public Guid? BroadcastId { get; set; }


    /// <summary />
    public BroadcastCancelCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.BroadcastCancelAsync( this.BroadcastId!.Value );

        return 0;
    }
}
