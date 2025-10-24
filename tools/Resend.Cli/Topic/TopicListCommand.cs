using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Topic;

/// <summary />
[Command( "list", Description = "List all topics." )]
public class TopicListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public TopicListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.TopicListAsync();
        var topics = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( topics, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Topic Id" );
            table.AddColumn( "Name" );
            table.AddColumn( "Description" );
            table.AddColumn( "Subscription" );
            table.AddColumn( "Created" );

            foreach ( var c in topics )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Name ),
                   new Markup( c.Description ),
                   new Markup( c.SubscriptionDefault.ToString() ),
                   new Markup( c.MomentCreated.ToString() )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}