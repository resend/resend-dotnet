using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Segment;

/// <summary />
[Command( "contacts", Description = "Lists all contacts in a segment" )]
public class SegmentContactListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Segment identifier" )]
    [Required]
    public Guid? SegmentId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public SegmentContactListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.SegmentListContactsAsync( this.SegmentId!.Value );
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
            table.AddColumn( "Contact Id" );
            table.AddColumn( "Email" );
            table.AddColumn( "First Name" );
            table.AddColumn( "Last Name" );
            table.AddColumn( "Created" );
            table.AddColumn( "Is Unsubscribed" );

            foreach ( var c in rows )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( Markup.Escape( c.Email ) ),
                   new Markup( Markup.Escape( c.FirstName ?? "" ) ),
                   new Markup( Markup.Escape( c.LastName ?? "" ) ),
                   new Markup( c.MomentCreated.ToShortDateString() ),
                   new Markup( IsUnsubscribed( c.IsUnsubscribed ) )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }


    /// <summary />
    private static string IsUnsubscribed( bool? isUnsubscribed )
    {
        if ( isUnsubscribed == null )
            return "";

        return isUnsubscribed == true ? "True" : "False";
    }
}
