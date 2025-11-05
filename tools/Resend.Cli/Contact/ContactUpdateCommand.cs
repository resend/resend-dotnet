using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Contact;

/// <summary />
[Command( "update", Description = "Update a contact" )]
public class ContactUpdateCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Contact identifier" )]
    [Required]
    public Guid? ContactId { get; set; }

    /// <summary />
    [Option( "-e|--email", CommandOptionType.SingleValue, Description = "Email" )]
    public string? Email { get; set; } = default!;

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
    public ContactUpdateCommand( IResend resend )
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

        await _resend.ContactUpdateAsync( this.ContactId!.Value, data );

        return 0;
    }
}
