using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Template;

/// <summary />
[Command( "delete", Description = "Delete a template" )]
public class TemplateDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Template identifier" )]
    [Required]
    public Guid? TemplateId { get; set; }


    /// <summary />
    public TemplateDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.TemplateDeleteAsync( this.TemplateId!.Value );

        return 0;
    }
}