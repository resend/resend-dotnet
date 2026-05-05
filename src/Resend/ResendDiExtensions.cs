using Microsoft.Extensions.DependencyInjection;

namespace Resend;

/// <summary>
/// Extension methods to quickly configure Resend for dependency
/// injection.
/// </summary>
public static class ResendDiExtensions
{
    /// <summary>
    /// Configures <see cref="IResend" /> for dependency injection.
    /// </summary>
    /// <param name="services">
    /// Dependency injection service collection.
    /// </param>
    /// <param name="apiToken">
    /// API token.
    /// </param>
    /// <returns>
    /// The <see cref="IHttpClientBuilder" /> so that additional calls can
    /// be chained.
    /// </returns>
    public static IHttpClientBuilder AddResend( this IServiceCollection services, string apiToken )
    {
        ArgumentNullException.ThrowIfNullOrEmpty( apiToken );

        return services.AddResend( options =>
        {
            options.ApiToken = apiToken;
        } );
    }


    /// <summary>
    /// Configures <see cref="IResend" /> for dependency injection.
    /// </summary>
    /// <param name="services">
    /// Dependency injection service collection.
    /// </param>
    /// <param name="configureOptions">
    /// Registers an action used to configure a particular type of options.
    /// </param>
    /// <returns>
    /// The <see cref="IHttpClientBuilder" /> so that additional calls can
    /// be chained.
    /// </returns>
    public static IHttpClientBuilder AddResend( this IServiceCollection services, Action<ResendClientOptions> configureOptions )
    {
        ArgumentNullException.ThrowIfNull( services );
        ArgumentNullException.ThrowIfNull( configureOptions );

        services.Configure( configureOptions );

        return services.AddHttpClient<IResend, ResendClient>();
    }
}