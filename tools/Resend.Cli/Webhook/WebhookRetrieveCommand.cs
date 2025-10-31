using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Webhook;

/// <summary />
[Command( "get", Description = "Retrieves a webhook" )]
public class WebhookRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Webhook identifier" )]
    [Required]
    public Guid? WebhookId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public WebhookRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.WebhookRetrieveAsync( this.WebhookId!.Value );
        var webhook = res.Content;


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( webhook, jso );

            Console.WriteLine( json );
        }
        else
        {
            // Record
            var record = new Table();
            record.Border = TableBorder.SimpleHeavy;
            record.AddColumn( "Webhook Id" );
            record.AddColumn( "Endpoint URL" );
            record.AddColumn( "Status" );

            record.AddRow(
               new Markup( webhook.Id.ToString() ),
               new Markup( webhook.EndpointUrl ),
               new Markup( webhook.Status.ToString() )
            );

            AnsiConsole.Write( record );

            // Events
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Event" );

            foreach ( var e in webhook.Events )
            {
                table.AddRow(
                   new Markup( e.ToString() )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}