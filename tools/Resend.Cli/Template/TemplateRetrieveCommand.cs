using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Template;

/// <summary />
[Command( "get", Description = "Retrieves a template" )]
public class TemplateRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Template identifier" )]
    [Required]
    public Guid? TemplateId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public TemplateRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.TemplateRetrieveAsync( this.TemplateId!.Value );
        var template = res.Content;


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( template, jso );

            Console.WriteLine( json );
        }
        else
        {
            // Record
            var record = new Table();
            record.Border = TableBorder.SimpleHeavy;
            record.AddColumn( "Template Id" );
            record.AddColumn( "Name" );
            record.AddColumn( "Alias" );
            record.AddColumn( "Status" );
            record.AddColumn( "Published" );

            record.AddRow(
                new Markup( template.Id.ToString() ),
                new Markup( template.Name ),
                new Markup( template.Alias ?? "" ),
                new Markup( template.Status.ToString() ),
                new Markup( template.MomentPublished?.ToString() ?? "" )
            );

            AnsiConsole.Write( record );
        }

        return 0;
    }
}