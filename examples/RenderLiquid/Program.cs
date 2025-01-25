using Resend;

namespace RenderLiquid;

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

        // Liquid

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

        app.UseStaticFiles();
        app.MapControllers();


        /*
         * 
         */
        app.Run();
    }
}
