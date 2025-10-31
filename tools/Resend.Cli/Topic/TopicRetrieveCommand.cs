using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Topic;

/// <summary />
[Command( "get", Description = "Retrieves a topic" )]
public class TopicRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Topic identifier" )]
    [Required]
    public Guid? TopicId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public TopicRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.TopicRetrieveAsync( this.TopicId!.Value );
        var topic = res.Content;


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( topic, jso );

            Console.WriteLine( json );
        }
        else
        {
            // Record
            var record = new Table();
            record.Border = TableBorder.SimpleHeavy;
            record.AddColumn( "Topic Id" );
            record.AddColumn( "Name" );
            record.AddColumn( "Description" );
            record.AddColumn( "Subscription" );
            record.AddColumn( "Created" );

            record.AddRow(
               new Markup( topic.Id.ToString() ),
               new Markup( topic.Name ),
               new Markup( topic.Description ),
               new Markup( topic.SubscriptionDefault.ToString() ),
               new Markup( topic.MomentCreated.ToString() )
            );

            AnsiConsole.Write( record );
        }

        return 0;
    }
}