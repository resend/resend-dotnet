using AsyncHangfire.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace AsyncHangfire.Controllers;

/// <summary />
[ApiController]
[Route( "[controller]" )]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    /// <summary />
    public TestController( ILogger<TestController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        await Task.Yield();


        /*
         * Prepare the message.
         */
        var message = new EmailMessage();
        message.From = "you@domain.com";
        message.To.Add( "user@gmail.com" );
        message.Subject = "Hello from Async Hangfire";
        message.TextBody = "Email using Resend .NET SDK";


        /*
         * 
         */
        var jobId = BackgroundJob.Enqueue<EmailSendJob>( x => x.BackgroundSendAsync( message, CancellationToken.None ) );

        _logger.LogInformation( "Email in background, as job {JobId}", jobId );

        return Ok();
    }
}
