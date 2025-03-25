using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Broadcast;

/// <summary />
[Command( "update", Description = "Update a broadcast" )]
public class BroadcastUpdateCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Broadcast identifier" )]
    [Required]
    public Guid? BroadcastId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Broadcast data" )]
    [FileExists]
    [Required]
    public string? Filename { get; set; }


    /// <summary />
    public BroadcastUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        var json = await File.ReadAllTextAsync( this.Filename! );
        var data = JsonSerializer.Deserialize<BroadcastUpdateData>( json );


        /*
         * 
         */
        var res = await _resend.BroadcastUpdateAsync( this.BroadcastId!.Value, data! );

        return 0;
    }
}