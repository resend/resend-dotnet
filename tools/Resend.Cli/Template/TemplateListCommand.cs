using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Template;

/// <summary />
[Command( "list", Description = "List all templates." )]
public class TemplateListCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public TemplateListCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.TemplateListAsync();
        var templates = res.Content.Data;

        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( templates, jso );

            Console.WriteLine( json );
        }
        else
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Template Id" );
            table.AddColumn( "Name" );
            table.AddColumn( "Alias" );
            table.AddColumn( "Status" );
            table.AddColumn( "Published" );

            foreach ( var c in templates )
            {
                table.AddRow(
                   new Markup( c.Id.ToString() ),
                   new Markup( c.Name ),
                   new Markup( c.Alias ?? "" ),
                   new Markup( c.Status.ToString() ),
                   new Markup( c.MomentPublished?.ToString() ?? "" )
                );
            }

            AnsiConsole.Write( table );
        }

        return 0;
    }
}