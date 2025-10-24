using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Topic;

/// <summary />
[Command( "add", Description = "Create a topic" )]
public class TopicAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Topic name" )]
    [Required]
    public string? Name { get; set; }

    /// <summary />
    [Argument( 1, Description = "Topic description" )]
    public string Description { get; set; } = "";

    /// <summary />
    [Option( "-s|--subscription", CommandOptionType.SingleValue, Description = "Subscription type" )]
    public SubscriptionType SubscriptionDefault { get; set; } = SubscriptionType.OptIn;


    /// <summary />
    public TopicAddCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var data = new TopicData()
        {
            Name = this.Name!,
            Description = this.Description,
            SubscriptionDefault = this.SubscriptionDefault,
        };

        var res = await _resend.TopicCreateAsync( data );
        var topicId = res.Content;


        /*
         * 
         */
        Console.WriteLine( topicId );

        return 0;
    }
}