using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Contact.Segment;

/// <summary />
[Command( "list", Description = "Lists all segments for a contact" )]
public class ContactSegmentListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Contact identifier" )]
    [Required]
    public Guid? ContactId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public ContactSegmentListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ContactListSegmentsAsync( this.ContactId!.Value );
        var rows = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( rows, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Segment Id" );
            table.AddColumn( "Name" );
            table.AddColumn( "Created" );

            foreach ( var c in rows )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Name ),
                   new Markup( c.MomentCreated.ToShortDateString() )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}