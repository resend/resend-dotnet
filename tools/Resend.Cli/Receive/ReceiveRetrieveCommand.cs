using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Receive;

/// <summary />
[Command( "get", Description = "Retrieves a received email" )]
public class ReceiveRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Email identifier" )]
    [Required]
    public Guid? ReceivedId { get; set; }


    /// <summary />
    public ReceiveRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ReceivedEmailRetrieveAsync( this.ReceivedId!.Value );
        var email = res.Content;


        /*
         * 
         */
        var jso = new JsonSerializerOptions() { WriteIndented = true };
        var json = JsonSerializer.Serialize( email, jso );

        Console.WriteLine( json );

        return 0;
    }
}