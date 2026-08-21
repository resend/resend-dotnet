using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Broadcast;

/// <summary />
[Command( "recipients", Description = "Lists a broadcast's recipients for a given event type" )]
public class BroadcastListRecipientsCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Broadcast identifier" )]
    [Required]
    public Guid? BroadcastId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Recipient event type" )]
    [Required]
    public BroadcastRecipientEventType? Type { get; set; }

    /// <summary />
    [Option( "-e|--email", CommandOptionType.SingleValue, Description = "Filter recipients whose email contains this value" )]
    public string? Email { get; set; }

    /// <summary />
    [Option( "--bounce-type", CommandOptionType.SingleValue, Description = "Filter bounced recipients by bounce type (only valid when type is bounced)" )]
    public BroadcastRecipientBounceType? BounceType { get; set; }

    /// <summary />
    [Option( "-l|--limit", CommandOptionType.SingleValue, Description = "Number of recipients to return" )]
    public int? Limit { get; set; }

    /// <summary />
    [Option( "-b|--before", CommandOptionType.SingleValue, Description = "Recipients before cursor" )]
    public string? BeforeId { get; set; }

    /// <summary />
    [Option( "-a|--after", CommandOptionType.SingleValue, Description = "Recipients after cursor" )]
    public string? AfterId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public BroadcastListRecipientsCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var q = new BroadcastListRecipientsQuery()
        {
            Email = this.Email,
            BounceType = this.BounceType,
            Limit = this.Limit,
            Before = this.BeforeId,
            After = this.AfterId,
        };

        var res = await _resend.BroadcastListRecipientsAsync( this.BroadcastId!.Value, this.Type!.Value, q );
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
            table.AddColumn( "Id" );
            table.AddColumn( "Contact Id" );
            table.AddColumn( "Email" );
            table.AddColumn( "Count" );
            table.AddColumn( "Bounce Type" );
            table.AddColumn( "Clicked Links" );

            foreach ( var d in results.Data )
            {
                table.AddRow(
                    new Markup( d.Id ),
                    new Markup( d.ContactId?.ToString() ?? "" ),
                    new Markup( d.Email ),
                    new Markup( d.Count?.ToString() ?? "" ),
                    new Markup( d.BounceType?.ToString() ?? "" ),
                    new Markup( d.ClickedLinks == null ? "" : string.Join( ", ", d.ClickedLinks.Select( x => $"{x.Url} ({x.Clicks})" ) ) )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}
