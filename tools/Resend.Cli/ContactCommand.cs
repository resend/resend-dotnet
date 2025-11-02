using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "contact", Description = "Contact management" )]
[Subcommand( typeof( Contact.ContactAddCommand ))]
[Subcommand( typeof( Contact.ContactRetrieveCommand ))]
[Subcommand( typeof( Contact.ContactUpdateCommand ) )]
[Subcommand( typeof( Contact.ContactDeleteCommand ) )]
[Subcommand( typeof( Contact.ContactListCommand ) )]
[Subcommand( typeof( Contact.ContactSegmentCommand ) )]
public class ContactCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}