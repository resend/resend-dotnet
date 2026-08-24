using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Broadcast;

/// <summary />
[Command( "clicked-links", Description = "Lists the clicked links of a broadcast" )]
public class BroadcastClickedLinksCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Broadcast identifier" )]
    [Required]
    public Guid? BroadcastId { get; set; }

    /// <summary />
    [Option( "-l|--limit", CommandOptionType.SingleValue, Description = "Number of links to return" )]
    public int? Limit { get; set; }

    /// <summary />
    [Option( "-b|--before", CommandOptionType.SingleValue, Description = "Links before cursor" )]
    public string? BeforeId { get; set; }

    /// <summary />
    [Option( "-a|--after", CommandOptionType.SingleValue, Description = "Links after cursor" )]
    public string? AfterId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public BroadcastClickedLinksCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var q = new PaginatedQuery()
        {
            Limit = this.Limit,
            Before = this.BeforeId,
            After = this.AfterId,
        };

        var res = await _resend.BroadcastClickedLinksAsync( this.BroadcastId!.Value, q );
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
            table.AddColumn( "Url" );
            table.AddColumn( "Clicks" );
            table.AddColumn( "Unique clicks" );

            foreach ( var c in rows )
            {
                table.AddRow(
                   new Markup( Markup.Escape( c.Url ) ),
                   new Markup( c.Clicks.ToString() ),
                   new Markup( c.UniqueClicks.ToString() )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}
