using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Receive;

/// <summary />
[Command( "list", Description = "List all received emails." )]
public class ReceiveListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Number of emails" )]
    public int? Limit { get; set; }

    /// <summary />
    [Option( "-b|--before", CommandOptionType.SingleValue, Description = "Emails sent before id" )]
    public string? BeforeId { get; set; }

    /// <summary />
    [Option( "-a|--after", CommandOptionType.SingleValue, Description = "Emails sent after id" )]
    public string? AfterId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public ReceiveListCommand( IResend resend )
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

        var res = await _resend.ReceivedEmailListAsync( q );
        var emails = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( emails, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Email Id" );
            table.AddColumn( "Subject" );
            table.AddColumn( "Date" );

            foreach ( var c in emails )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Subject ),
                   new Markup( c.MomentCreated?.ToString() ?? "" )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}