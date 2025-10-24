using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Topic;

/// <summary />
[Command( "update", Description = "Update a topic" )]
public class TopicUpdateCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Topic identifier" )]
    [Required]
    public Guid? TopicId { get; set; }

    /// <summary />
    [Option( "-n|--name", CommandOptionType.SingleValue, Description = "Name of topic" )]
    public string? Name { get; set; } = default!;

    /// <summary />
    [Option( "-d|--description", CommandOptionType.SingleValue, Description = "Description" )]
    public string? Description { get; set; } = default!;

    /// <summary />
    [Option( "-s|--subscription", CommandOptionType.SingleValue, Description = "Subscription type" )]
    public SubscriptionType? SubscriptionDefault { get; set; }


    /// <summary />
    public TopicUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        var req1 = await _resend.TopicRetrieveAsync( this.TopicId!.Value );
        var t = req1.Content;


        /*
         * 
         */
        var data = new TopicData()
        {
            Name = this.Name ?? t.Name,
            Description = this.Description ?? t.Description,
            SubscriptionDefault = this.SubscriptionDefault ?? t.SubscriptionDefault,
        };

        await _resend.TopicUpdateAsync( this.TopicId!.Value, data );

        return 0;
    }
}