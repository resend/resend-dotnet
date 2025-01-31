using Microsoft.Extensions.DependencyInjection;
using Resend.Webhooks;

namespace Microsoft.AspNetCore.Builder;

/// <summary />
public static class WebApplicationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddResendWebhooks( this IServiceCollection services, Action<WebhookValidatorOptions>? configureOptions = null )
    {
        services.AddOptions();
        services.AddOptions<WebhookValidatorOptions>();
        services.AddTransient<WebhookValidator>();

        if ( configureOptions != null )
            services.Configure<WebhookValidatorOptions>( configureOptions );

        return services;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="app"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapResendWebhook<T>( this WebApplication app, string path )
        where T : notnull, IWebhookHandler
    {
        app.MapPost( path, WebhookSink<T>.ExecuteAsync );

        return app;
    }
}
