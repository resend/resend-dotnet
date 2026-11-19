using Resend;
using WorkerService;


/*
 * 
 */
var builder = Host.CreateApplicationBuilder( args );

builder.Services.AddResend( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
} );

builder.Services.Configure<EmailWorkerOptions>( builder.Configuration.GetSection( "EmailWorker" ) );
builder.Services.AddHostedService<EmailWorker>();


/*
 * 
 */
var host = builder.Build();

await host.RunAsync();
