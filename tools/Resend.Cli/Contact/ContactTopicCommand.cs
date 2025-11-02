using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli.Contact;

/// <summary />
[Command( "topic", Description = "Contact topic management" )]
[Subcommand( typeof( Topic.ContactTopicSetCommand ) )]
[Subcommand( typeof( Topic.ContactTopicListCommand ) )]
public class ContactTopicCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}