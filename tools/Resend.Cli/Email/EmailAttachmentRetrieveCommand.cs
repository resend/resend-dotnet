using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Email;

/// <summary />
[Command( "get", Description = "Retrieves an attachment from a sent email" )]
public class EmailAttachmentRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Email identifier" )]
    [Required]
    public Guid? EmailId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Attachment identifier" )]
    [Required]
    public Guid? AttachmentId { get; set; }


    /// <summary />
    public EmailAttachmentRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.EmailAttachmentRetrieveAsync( this.EmailId!.Value, this.AttachmentId!.Value );
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