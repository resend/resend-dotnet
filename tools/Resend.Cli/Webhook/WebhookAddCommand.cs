using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Webhook;

/// <summary />
[Command( "add", Description = "Create a webhook" )]
public class WebhookAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Endpoint URL." )]
    [Required]
    public string? EndpoointUrl { get; set; }

    /// <summary />
    [Option( "-t|--type", CommandOptionType.SingleValue, Description = "Event type" )]
    public WebhookEventType EventType { get; set; } = WebhookEventType.EmailSent;


    /// <summary />
    public WebhookAddCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var data = new WebhookData()
        {
            EndpointUrl = this.EndpoointUrl!,
            Events = [ this.EventType ],
            Status = WebhookStatus.Disabled,
        };

        var res = await _resend.WebhookCreateAsync( data );
        var wh = res.Content;


        /*
         * 
         */
        Console.WriteLine( wh.Id );
        Console.WriteLine( wh.SigningSecret );

        return 0;
    }
}