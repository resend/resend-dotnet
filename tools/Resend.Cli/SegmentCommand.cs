using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "segment", Description = "Segments management" )]
[Subcommand( typeof( Segment.SegmentAddCommand ) )]
[Subcommand( typeof( Segment.SegmentDeleteCommand ) )]
[Subcommand( typeof( Segment.SegmentListCommand ) )]
[Subcommand( typeof( Segment.SegmentRetrieveCommand ) )]
public class SegmentCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}