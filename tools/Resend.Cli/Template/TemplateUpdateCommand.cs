using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Template;

/// <summary />
[Command( "update", Description = "Update a template" )]
public class TemplateUpdateCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Template identifier" )]
    [Required]
    public Guid? TemplateId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Input JSON file" )]
    [FileExists]
    public string? InputFile { get; set; }


    /// <summary />
    public TemplateUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        string json;

        if ( Console.IsInputRedirected == true && this.InputFile == null )
        {
            json = await Console.In.ReadToEndAsync();
        }
        else
        {
            var ifile = this.InputFile ?? "template.json";

            if ( File.Exists( ifile ) == false )
            {
                var of = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( "The file '{0}' does not exist.", ifile );
                Console.ForegroundColor = of;

                return 1;
            }

            json = File.ReadAllText( ifile );
        }

        TemplateData data = JsonSerializer.Deserialize<TemplateData>( json )!;


        /*
         * 
         */
        await _resend.TemplateUpdateAsync( this.TemplateId!.Value, data );

        return 0;
    }
}