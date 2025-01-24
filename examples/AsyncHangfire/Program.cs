using Hangfire;
using Hangfire.Storage.SQLite;
using Resend;

namespace AsyncHangfire;

/// <summary />
public class Program
{
    /// <summary />
    public static void Main( string[] args )
    {
        /*
         * 
         */
        var builder = WebApplication.CreateBuilder( args );

        builder.Services.AddControllers();

        // Hangfire
        builder.Services.AddHangfire( cfg =>
        {
            cfg.SetDataCompatibilityLevel( CompatibilityLevel.Version_180 );
            cfg.UseSimpleAssemblyNameTypeSerializer();
            cfg.UseRecommendedSerializerSettings();
            cfg.UseSQLiteStorage( "z-jobs.db" );
        } );
        builder.Services.AddHangfireServer();

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

        app.UseHangfireDashboard( "/jobs", new DashboardOptions()
        {
            DashboardTitle = "Async Hangfire",
            DarkModeEnabled = true,
            DefaultRecordsPerPage = 20,
        } );
        app.MapControllers();


        /*
         * 
         */
        app.Run();
    }
}