using AsyncTemporal.Temporal;
using Resend;
using Temporalio.Extensions.Hosting;

namespace AsyncTemporal;

/// <summary />
public class Program
{
    /// <summary />
    public static async Task<int> Main( string[] args )
    {
        /*
         *
         */
        var builder = WebApplication.CreateBuilder( args );

        builder.Services.AddControllers();

        // Temporal (Client)
        builder.Services.AddTemporalClient(
            clientTargetHost: "localhost:7233",
            clientNamespace: "default" );

        // Temporal (Worker)
        builder.Services.AddHostedTemporalWorker(
            clientTargetHost: "localhost:7233",
            clientNamespace: "default",
            taskQueue: TemporalWorker.TaskQueue )
            .AddScopedActivities<EmailSendActivity>()
            .AddWorkflow<EmailSendWorkflow>();


        // Resend
        builder.Services.AddOptions();
        builder.Services.Configure<ResendClientOptions>( o =>
        {
            o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
        } );
        builder.Services.AddHttpClient<ResendClient>();
        builder.Services.AddTransient<IResend, ResendClient>();


        /*
         * 
         */
        var app = builder.Build();

        app.UseAuthorization();
        app.MapControllers();


        /*
         * Make sure you use RunAsync and not Run
         * see https://github.com/temporalio/sdk-dotnet/issues/220
         */
        await app.RunAsync();

        return 0;
    }
}
