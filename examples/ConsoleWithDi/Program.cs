using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resend;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var config = context.Configuration;
                services.AddOptions();
                services.AddHttpClient<ResendClient>();
                services.Configure<ResendClientOptions>(o =>
                {
                    o.ApiToken = config["Resend:ApiToken"];
                });

                services.AddTransient<IResend, ResendClient>();
                services.AddTransient<App>();
            })
            .Build();

        var app = host.Services.GetRequiredService<App>();
        await app.Run();
    }

}