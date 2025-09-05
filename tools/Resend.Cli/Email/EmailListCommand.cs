using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Email;

/// <summary />
[Command( "list", Description = "Lists emails which were previously sent" )]
public class EmailListCommand
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
    public EmailListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var q = new PaginatedQuery()
        {
            Limit = this.Limit,
            BeforeId = this.BeforeId,
            AfterId = this.AfterId,
        };

        var res = await _resend.EmailListAsync( q );
        var results = res.Content;


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true, };

            var json = JsonSerializer.Serialize( results, jso );
            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Email Id" );
            table.AddColumn( "Subject" );
            table.AddColumn( "Date" );
            table.AddColumn( "Last Event" );

            foreach ( var d in results.Data )
            {
                table.AddRow(
                    new Markup( d.Id.ToString() ),
                    new Markup( d.Subject ),
                    new Markup( d.MomentCreated.ToString( "yyyy-MM-dd" ) ),
                    new Markup( d.LastEvent?.ToString() ?? "" )
                    );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}