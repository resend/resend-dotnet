using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Email;

/// <summary />
[Command( "batch", Description = "Sends a batch of emails" )]
public class EmailBatchCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Input JSON file" )]
    [FileExists]
    public string? InputFile { get; set; }

    /// <summary />
    [Option( "-k|--key", CommandOptionType.SingleValue, Description = "Idempotency key" )]
    public string? IdempotencyKey { get; set; }

    /// <summary />
    [Option( "-m|--mode", CommandOptionType.SingleValue, Description = "Validation mode" )]
    public EmailBatchValidationMode ValidationMode { get; set; } = EmailBatchValidationMode.Strict;

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public EmailBatchCommand( IResend resend )
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
            var ifile = this.InputFile ?? "emails.json";

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

        List<EmailMessage> messages = JsonSerializer.Deserialize<List<EmailMessage>>( json )!;


        /*
         * 
         */
        ResendResponse<EmailBatchResponse> res;

        if ( this.IdempotencyKey != null )
            res = await _resend.EmailBatchAsync( this.IdempotencyKey, messages, this.ValidationMode );
        else
            res = await _resend.EmailBatchAsync( messages, this.ValidationMode );


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var ojson = JsonSerializer.Serialize( res.Content, jso );

            Console.WriteLine( ojson );
        }
        else
        {
            if ( res.Content.Data.Count > 0 )
            {
                var table = new Table();
                table.Border = TableBorder.SimpleHeavy;
                table.AddColumn( "Id" );

                foreach ( var d in res.Content.Data )
                {
                    table.AddRow(
                        new Markup( d.Id.ToString() )
                    );
                }

                AnsiConsole.Write( table );
            }

            if ( res.Content.Errors != null )
            {
                var table = new Table();
                table.Border = TableBorder.SimpleHeavy;
                table.AddColumn( "Ix" );
                table.AddColumn( "Error" );

                foreach ( var d in res.Content.Errors )
                {
                    table.AddRow(
                        new Markup( d.Index.ToString() ),
                        new Markup( d.Message )
                    );
                }

                AnsiConsole.Write( table );
            }
        }

        return 0;
    }
}
