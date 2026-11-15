using Microsoft.Extensions.Options;
using Resend;

namespace WorkerService;

/// <summary />
public class EmailWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly EmailWorkerOptions _options;
    private readonly ILogger<EmailWorker> _logger;


    /// <summary />
    public EmailWorker( IServiceScopeFactory scopeFactory, IOptions<EmailWorkerOptions> options, ILogger<EmailWorker> logger )
    {
        _scopeFactory = scopeFactory;
        _options = options.Value;
        _logger = logger;
    }


    /// <summary />
    protected override async Task ExecuteAsync( CancellationToken stoppingToken )
    {
        using var timer = new PeriodicTimer( _options.Interval );

        do
        {
            await SendAsync( stoppingToken );
        }
        while ( await timer.WaitForNextTickAsync( stoppingToken ) );
    }


    /// <summary />
    private async Task SendAsync( CancellationToken cancellationToken )
    {
        /*
         * The worker is a singleton, but IResend depends on scoped options:
         * resolve it from a new scope on every send.
         */
        await using var scope = _scopeFactory.CreateAsyncScope();
        var resend = scope.ServiceProvider.GetRequiredService<IResend>();

        var message = new EmailMessage();
        message.From = _options.From;
        message.To.Add( _options.To );
        message.Subject = "Hello from Worker Service";
        message.TextBody = "Email using Resend .NET SDK";


        /*
         * A failed send is logged, and the worker carries on with the next
         * tick: an unhandled exception would stop the whole host.
         */
        try
        {
            var resp = await resend.EmailSendAsync( message, cancellationToken );

            _logger.LogInformation( "Sent email, with Id = {EmailId}", resp.Content );
        }
        catch ( ResendException ex )
        {
            _logger.LogError( "Failed to send email: {ErrorType} - {Message}", ex.ErrorType, ex.Message );
        }
    }
}
