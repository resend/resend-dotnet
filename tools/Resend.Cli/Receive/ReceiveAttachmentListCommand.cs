using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Receive;

/// <summary />
[Command( "list", Description = "List all received emails" )]
public class ReceiveAttachmentListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Email identifier" )]
    [Required]
    public Guid? ReceivedId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public ReceiveAttachmentListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ReceivedEmailAttachmentListAsync( this.ReceivedId!.Value );
        var attachs = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( attachs, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Attach Id" );
            table.AddColumn( "Filename" );
            table.AddColumn( "File size" );
            table.AddColumn( "Expiration" );

            foreach ( var c in attachs )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Filename ),
                   new Markup( c.FileSize?.ToString() ?? "" ),
                   new Markup( c.MomentExpiration?.ToString() ?? "" )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}