using AsyncTemporal.Temporal;
using Microsoft.AspNetCore.Mvc;
using Resend;
using Temporalio.Client;

namespace AsyncTemporal.Controllers;

/// <summary />
[ApiController]
[Route( "[controller]" )]
public class TestController : ControllerBase
{
    private readonly ITemporalClient _temporal;
    private readonly ILogger<TestController> _logger;


    /// <summary />
    public TestController( ITemporalClient temporal, ILogger<TestController> logger )
    {
        _temporal = temporal;
        _logger = logger;
    }


    /// <summary />
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        /*
         * Prepare the message.
         */
        var message = new EmailMessage();
        message.From = "you@domain.com";
        message.To.Add( "user@gmail.com" );
        message.Subject = "Hello from Async Temporal";
        message.TextBody = "Email using Resend .NET SDK";


        /*
         * 
         */
        var instanceId = Guid.NewGuid().ToString().ToLowerInvariant();

        var handle = await _temporal.StartWorkflowAsync(
            ( EmailSendWorkflow wf ) => wf.RunAsync( message ),
            new WorkflowOptions( id: $"resend-{instanceId}", taskQueue: TemporalWorker.TaskQueue ) );

        _logger.LogInformation( "Email: Temporal {HandleId} - {RunId}", handle.Id, handle.RunId );

        return Ok();
    }
}