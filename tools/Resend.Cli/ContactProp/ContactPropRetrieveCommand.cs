using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.ContactProp;

/// <summary />
[Command( "get", Description = "Retrieves a contact property" )]
public class ContactPropRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Property identifier" )]
    [Required]
    public Guid? PropId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public ContactPropRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ContactPropRetrieveAsync( this.PropId!.Value );
        var prop = res.Content;


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( prop, jso );

            Console.WriteLine( json );
        }
        else
        {
            // Record
            var record = new Table();
            record.Border = TableBorder.SimpleHeavy;
            record.AddColumn( "Prop Id" );
            record.AddColumn( "Key" );
            record.AddColumn( "Type" );
            record.AddColumn( "Default" );
            record.AddColumn( "Created" );

            record.AddRow(
               new Markup( prop.Id.ToString() ),
               new Markup( prop.Key ),
               new Markup( prop.PropertyType.ToString() ),
               new Markup( prop.DefaultValue?.ToString() ?? "" ),
               new Markup( prop.MomentCreated.ToString() )
            );

            AnsiConsole.Write( record );
        }

        return 0;
    }
}