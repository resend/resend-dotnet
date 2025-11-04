using McMaster.Extensions.CommandLineUtils;
using System.Text.Json;

namespace Resend.Cli.Email;

/// <summary />
[Command( "skel", Description = "Create a skeleton email JSON file" )]
public class EmailSkeletonCommand
{
    /// <summary />
    [Option( "-o|--output", CommandOptionType.SingleValue, Description = "Output filename" )]
    public string OutputFilename { get; set; } = "out.json";


    /// <summary />
    public EmailSkeletonCommand()
    {
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        var email = new EmailMessage()
        {
            From = "onboarding@resend.dev",
            To = "delivered@resend.dev",
            Subject = "Subject",
            HtmlBody = "HTML body",
        };


        /*
         * 
         */
        var json = JsonSerializer.Serialize( email , new JsonSerializerOptions()
        {
            WriteIndented = true,
        } );

        await File.WriteAllTextAsync( this.OutputFilename, json );
        Console.WriteLine( "skeleton file written to '{0}'", this.OutputFilename );

        return 0;
    }
}