using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Segment;

/// <summary />
[Command( "list", Description = "List all topics" )]
public class SegmentListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public SegmentListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.SegmentListAsync();
        var segments = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( segments, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Segment Id" );
            table.AddColumn( "Name" );
            table.AddColumn( "Created" );

            foreach ( var c in segments )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Name ),
                   new Markup( c.MomentCreated.ToString() )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}