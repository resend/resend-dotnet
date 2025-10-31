using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Webhook;

/// <summary />
[Command( "delete", Description = "Delete a webhook" )]
public class WebhookDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Webhook identifier" )]
    [Required]
    public Guid? WebhookId { get; set; }


    /// <summary />
    public WebhookDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.WebhookDeleteAsync( this.WebhookId!.Value );

        return 0;
    }
}