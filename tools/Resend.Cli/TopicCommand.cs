using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "topic", Description = "Topics management" )]
[Subcommand( typeof( Topic.TopicAddCommand ) )]
[Subcommand( typeof( Topic.TopicDeleteCommand ) )]
[Subcommand( typeof( Topic.TopicListCommand ) )]
[Subcommand( typeof( Topic.TopicRetrieveCommand ) )]
[Subcommand( typeof( Topic.TopicUpdateCommand ) )]
public class TopicCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}