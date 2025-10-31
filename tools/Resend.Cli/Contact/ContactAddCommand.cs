using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Contact;

/// <summary />
[Command( "add", Description = "Create a contact" )]
public class ContactAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Audience identifier" )]
    [Required]
    public Guid? AudienceId { get; set; }

    /// <summary />
    [Option( "-e|--email", CommandOptionType.SingleValue, Description = "Email" )]
    [Required]
    public string Email { get; set; } = default!;

    /// <summary />
    [Option( "-f|--first", CommandOptionType.SingleValue, Description = "First name" )]
    public string? FirstName { get; set; }

    /// <summary />
    [Option( "-l|--last", CommandOptionType.SingleValue, Description = "Last name" )]
    public string? LastName { get; set; }

    /// <summary />
    [Option( "-u|--unsubscribed", CommandOptionType.SingleValue, Description = "Unsubscribed" )]
    public bool? IsUnsubscribed { get; set; }


    /// <summary />
    public ContactAddCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var data = new ContactData()
        {
            Email = this.Email,
            FirstName = this.FirstName,
            LastName = this.LastName,
            IsUnsubscribed = this.IsUnsubscribed,
        };

        var res = await _resend.ContactAddAsync( this.AudienceId!.Value, data );
        var id = res.Content;


        /*
         * 
         */
        Console.WriteLine( id );

        return 0;
    }
}
