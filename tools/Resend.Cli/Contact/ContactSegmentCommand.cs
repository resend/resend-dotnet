using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli.Contact;

/// <summary />
[Command( "segment", Description = "Contact segment management" )]
[Subcommand( typeof( Segment.ContactSegmentAddCommand ) )]
[Subcommand( typeof( Segment.ContactSegmentDeleteCommand ) )]
[Subcommand( typeof( Segment.ContactSegmentListCommand ) )]
public class ContactSegmentCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}