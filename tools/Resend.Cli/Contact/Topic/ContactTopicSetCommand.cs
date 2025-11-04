using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Contact.Topic;

/// <summary />
[Command( "set", Description = "Sets a topic subscription for a contact" )]
public class ContactTopicSetCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Contact identifier" )]
    [Required]
    public Guid? ContactId { get; set; }

    /// <summary />
    [Argument( 1, Description = "Topic identifier" )]
    [Required]
    public Guid? TopicId { get; set; }

    /// <summary />
    [Argument( 2, Description = "Subscription type" )]
    public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.OptIn;


    /// <summary />
    public ContactTopicSetCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * Fetch current list
         */
        var res = await _resend.ContactListTopicsAsync( this.ContactId!.Value );
        var subs = res.Content.Data;


        /*
         * 
         */
        var s = subs.FirstOrDefault( x => x.Id == this.TopicId );

        if ( s == null )
            throw new ApplicationException( $"Topic '{this.TopicId}' not found." );

        s.Subscription = this.SubscriptionType;


        /*
         * 
         */
        await _resend.ContactUpdateTopicsAsync( this.ContactId!.Value, subs );

        return 0;
    }
}