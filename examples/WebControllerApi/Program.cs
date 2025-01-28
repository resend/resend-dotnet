using Resend;

namespace WebControllerApi;

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

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

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
         * 
         */
        app.Run();
    }
}