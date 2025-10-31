using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Template;

/// <summary />
[Command( "duplicate", Description = "Duplicate a template" )]
public class TemplateDuplicateCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Template identifier" )]
    [Required]
    public Guid? TemplateId { get; set; }


    /// <summary />
    public TemplateDuplicateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.TemplateDuplicateAsync( this.TemplateId!.Value );
        Console.WriteLine( res.Content );

        return 0;
    }
}