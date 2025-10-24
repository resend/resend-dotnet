using McMaster.Extensions.CommandLineUtils;
using System.Text.Json;

namespace Resend.Cli.Template;

/// <summary />
[Command( "add", Description = "Create a template" )]
public class TemplateAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Input JSON file" )]
    [FileExists]
    public string? InputFile { get; set; }


    /// <summary />
    public TemplateAddCommand( IResend resend )
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
        var res = await _resend.TemplateCreateAsync( data );
        var topicId = res.Content;


        /*
         * 
         */
        Console.WriteLine( topicId );

        return 0;
    }
}