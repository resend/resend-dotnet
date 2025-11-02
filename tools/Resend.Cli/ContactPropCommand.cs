using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "contact-prop", Description = "Contact property anagement" )]
[Subcommand( typeof( ContactProp.ContactPropAddCommand ))]
[Subcommand( typeof( ContactProp.ContactPropRetrieveCommand ))]
[Subcommand( typeof( ContactProp.ContactPropUpdateCommand ) )]
[Subcommand( typeof( ContactProp.ContactPropDeleteCommand ) )]
[Subcommand( typeof( ContactProp.ContactPropListCommand ) )]
public class ContactPropCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}