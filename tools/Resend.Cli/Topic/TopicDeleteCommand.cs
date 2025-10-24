using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Topic;

/// <summary />
[Command( "delete", Description = "Delete a topic" )]
public class TopicDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Topic identifier" )]
    [Required]
    public Guid? TopicId { get; set; }


    /// <summary />
    public TopicDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.TopicDeleteAsync( this.TopicId!.Value );

        return 0;
    }
}