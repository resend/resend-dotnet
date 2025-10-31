using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Broadcast;

/// <summary />
[Command( "list", Description = "List all broadcasts" )]
public class BroadcastListCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public BroadcastListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        var res = await _resend.BroadcastListAsync();
        var contacts = res.Content;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( contacts, jso );

            Console.WriteLine( json );
        }
        else
        {
            var res2 = await _resend.AudienceListAsync();
            var audiences = res2.Content;


            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Broadcast Id" );
            table.AddColumn( "Audience" );
            table.AddColumn( "Name" );
            table.AddColumn( "Status" );
            table.AddColumn( "Created" );
            table.AddColumn( "Scheduled" );
            table.AddColumn( "Sent" );

            foreach ( var c in contacts )
            {
                var aud = audiences.SingleOrDefault( x => x.Id == c.AudienceId );

                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( aud?.Name ?? c.AudienceId.ToString() ),
                   new Markup( c.DisplayName ?? "" ),
                   new Markup( c.Status.ToString() ),
                   new Markup( c.MomentCreated.ToShortDateString() ),
                   new Markup( c.MomentScheduled?.ToShortDateString() ?? "" ),
                   new Markup( c.MomentSent?.ToShortDateString() ?? "" )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}
