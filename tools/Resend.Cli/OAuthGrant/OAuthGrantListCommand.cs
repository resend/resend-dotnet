using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.OAuthGrant;

/// <summary />
[Command( "list", Description = "Lists OAuth grants" )]
public class OAuthGrantListCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public OAuthGrantListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.OAuthGrantListAsync();
        var grants = res.Content.Data;


        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( grants, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Grant Id" );
            table.AddColumn( "Client" );
            table.AddColumn( "Scopes" );
            table.AddColumn( "Created" );

            foreach ( var g in grants )
            {
                table.AddRow(
                    new Markup( g.Id.ToString() ),
                    new Markup( Markup.Escape( g.Client.Name ) ),
                    new Markup( Markup.Escape( string.Join( ", ", g.Scopes ) ) ),
                    new Markup( g.MomentCreated.ToString() )
                    );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}
