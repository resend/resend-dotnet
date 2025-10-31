using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "template", Description = "Templates management" )]
[Subcommand( typeof( Template.TemplateAddCommand ) )]
[Subcommand( typeof( Template.TemplateDeleteCommand ) )]
[Subcommand( typeof( Template.TemplateDuplicateCommand ) )]
[Subcommand( typeof( Template.TemplateListCommand ) )]
[Subcommand( typeof( Template.TemplatePublishCommand ) )]
[Subcommand( typeof( Template.TemplateRetrieveCommand ) )]
[Subcommand( typeof( Template.TemplateUpdateCommand ) )]
public class TemplateCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}