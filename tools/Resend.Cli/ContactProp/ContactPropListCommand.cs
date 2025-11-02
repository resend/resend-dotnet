using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.ContactProp;

/// <summary />
[Command( "list", Description = "List all contact properties" )]
public class ContactPropListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public ContactPropListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ContactPropListAsync();
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
            table.AddColumn( "Prop Id" );
            table.AddColumn( "Key" );
            table.AddColumn( "Type" );
            table.AddColumn( "Default" );
            table.AddColumn( "Created" );

            foreach ( var c in rows )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Key ),
                   new Markup( c.PropertyType.ToString() ),
                   new Markup( c.DefaultValue?.ToString() ?? "" ),
                   new Markup( c.MomentCreated.ToString() )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}