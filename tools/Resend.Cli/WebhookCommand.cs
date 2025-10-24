using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "webhook", Description = "Webhook management" )]
[Subcommand( typeof( Webhook.WebhookAddCommand ) )]
[Subcommand( typeof( Webhook.WebhookDeleteCommand ) )]
[Subcommand( typeof( Webhook.WebhookListCommand ) )]
[Subcommand( typeof( Webhook.WebhookPostCommand ) )]
[Subcommand( typeof( Webhook.WebhookRetrieveCommand ) )]
[Subcommand( typeof( Webhook.WebhookUpdateCommand ) )]
public class WebhookCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}