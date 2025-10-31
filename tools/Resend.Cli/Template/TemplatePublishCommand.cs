using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Template;

/// <summary />
[Command( "publish", Description = "Publish a template" )]
public class TemplatePublishCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Template identifier" )]
    [Required]
    public Guid? TemplateId { get; set; }


    /// <summary />
    public TemplatePublishCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.TemplatePublishAsync( this.TemplateId!.Value );

        return 0;
    }
}