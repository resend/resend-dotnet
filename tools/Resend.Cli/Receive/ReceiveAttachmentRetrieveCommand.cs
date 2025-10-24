using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Receive;

/// <summary />
[Command( "get", Description = "Retrieves an attachment from a received email" )]
public class ReceiveAttachmentRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Email identifier" )]
    [Required]
    public Guid? ReceivedId { get; set; }

    /// <summary />
    [Argument( 0, Description = "Attachment identifier" )]
    [Required]
    public Guid? AttachmentId { get; set; }


    /// <summary />
    public ReceiveAttachmentRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ReceivedEmailAttachmentRetrieveAsync( this.ReceivedId!.Value, this.AttachmentId!.Value );
        var attach = res.Content;


        /*
         * 
         */
        var jso = new JsonSerializerOptions() { WriteIndented = true };
        var json = JsonSerializer.Serialize( attach, jso );

        Console.WriteLine( json );

        return 0;
    }
}