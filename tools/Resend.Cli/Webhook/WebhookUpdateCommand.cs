using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Webhook;

/// <summary />
[Command( "update", Description = "Update a webhook" )]
public class WebhookUpdateCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Webhook identifier" )]
    [Required]
    public Guid? WebhookId { get; set; }

    /// <summary />
    [Option( "-u|--url", CommandOptionType.SingleValue, Description = "Endpoint URL" )]
    public string? EndpointUrl { get; set; } = default!;

    /// <summary />
    [Option( "-t|--type", CommandOptionType.SingleValue, Description = "Event type" )]
    public WebhookEventType? EventType { get; set; }

    /// <summary />
    [Option( "-s|--status", CommandOptionType.SingleValue, Description = "Status" )]
    public WebhookStatus? Status { get; set; }


    /// <summary />
    public WebhookUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        var req1 = await _resend.WebhookRetrieveAsync( this.WebhookId!.Value );
        var wh = req1.Content;


        /*
         * 
         */
        var data = new WebhookData()
        {
            EndpointUrl = this.EndpointUrl ?? wh.EndpointUrl,
            Events = ToEvents( this.EventType, wh.Events ),
            Status = this.Status ?? wh.Status,
        };

        await _resend.WebhookUpdateAsync( this.WebhookId!.Value, data );

        return 0;
    }


    /// <summary />
    private List<WebhookEventType> ToEvents( WebhookEventType? @event, List<WebhookEventType> events )
    {
        if ( @event.HasValue == true )
            return [ @event.Value ];

        return events;
    }
}