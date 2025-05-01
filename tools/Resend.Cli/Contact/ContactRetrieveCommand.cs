using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Contact;

/// <summary />
[Command( "get", Description = "Retrieves the contact" )]
public class ContactRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Audience identifier" )]
    [Required]
    public Guid? AudienceId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Contact identifier or email" )]
    [Required]
    public string? Contact { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public ContactRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        Resend.Contact contact;

        if ( this.Contact!.Contains( "@" ) == true )
        {
            var res = await _resend.ContactRetrieveByEmailAsync( this.AudienceId!.Value, this.Contact! );
            contact = res.Content;
        }
        else
        {
            if ( Guid.TryParse( this.Contact!, out var contactId ) == false )
            {
                Console.WriteLine( "err: Contact is not a valid Guid" );
                return 1;
            }

            var res = await _resend.ContactRetrieveAsync( this.AudienceId!.Value, contactId );
            contact = res.Content;
        }


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( contact, jso );

            Console.WriteLine( json );
        }
        else
        {
            var record = new Table();
            record.Border = TableBorder.SimpleHeavy;
            record.AddColumn( "Contact Id" );
            record.AddColumn( "Email" );
            record.AddColumn( "First Name" );
            record.AddColumn( "Last Name" );
            record.AddColumn( "Created" );
            record.AddColumn( "Is Unsubscribed" );

            record.AddRow(
               new Markup( contact.Id.ToString() ),
               new Markup( contact.Email ),
               new Markup( contact.FirstName != null ? contact.FirstName : "" ),
               new Markup( contact.LastName != null ? contact.LastName : "" ),
               new Markup( contact.MomentCreated.ToShortDateString() ),
               new Markup( IsUnsubscribed( contact.IsUnsubscribed ) )
               );

            AnsiConsole.Write( record );
        }

        return 0;
    }


    /// <summary />
    private static string IsUnsubscribed( bool? isUnsubscribed )
    {
        if ( isUnsubscribed == null )
            return "";

        return isUnsubscribed == true ? "True" : "False";
    }
}