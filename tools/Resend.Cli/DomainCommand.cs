using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "domain", Description = "Email (sender) domain management" )]
[Subcommand( typeof( Domain.DomainAddCommand ))]
[Subcommand( typeof( Domain.DomainClaimCommand ) )]
[Subcommand( typeof( Domain.DomainDeleteCommand ))]
[Subcommand( typeof( Domain.DomainListCommand ) )]
[Subcommand( typeof( Domain.DomainRetrieveCommand ) )]
[Subcommand( typeof( Domain.DomainUpdateCommand ) )]
[Subcommand( typeof( Domain.DomainVerifyCommand ))]
public class DomainCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}
