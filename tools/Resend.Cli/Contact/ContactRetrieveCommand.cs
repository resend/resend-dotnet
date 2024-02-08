﻿using McMaster.Extensions.CommandLineUtils;
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
    [Argument( 0, Description = "The Audience identifier" )]
    [Required]
    public Guid AudienceId { get; set; }

    /// <summary />
    [Argument( 1, Description = "The Contact identifier" )]
    [Required]
    public Guid ContactId { get; set; }

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
        var res = await _resend.ContactRetrieveAsync( this.AudienceId, this.ContactId );
        var contact = res.Content;


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
            record.AddColumn( "Unsubscribed" );

            record.AddRow(
                new Markup( contact.Id.ToString() ),
                new Markup( contact.Email ),
                new Markup( contact.FirstName! ),
                new Markup( contact.LastName! ),
                new Markup( contact.Created.ToShortTimeString()),
                new Markup( contact.Unsubscribed!.ToString()!)
                );

            AnsiConsole.Write( record );
        }

        return 0;
    }
}
