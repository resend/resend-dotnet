using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Webhook;

/// <summary />
[Command( "list", Description = "List all webhooks." )]
public class WebhookListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public WebhookListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.WebhookListAsync();
        var webhooks = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( webhooks, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Webhook Id" );
            table.AddColumn( "Endpoint URL" );
            table.AddColumn( "Status" );
            table.AddColumn( "Events" );

            foreach ( var c in webhooks )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.EndpointUrl ),
                   new Markup( c.Status.ToString() ),
                   new Markup( ToEvents( c.Events ) )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }


    /// <summary />
    private string ToEvents( List<WebhookEventType> events )
    {
        if ( events.Count == 0 )
            return "";

        if ( events.Count > 1 )
            return events.Count.ToString();

        return events.First().ToString();
    }
}