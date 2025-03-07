using FluentEmail.Core.Interfaces;
using Microsoft.Extensions.Options;
using Resend;
using Resend.FluentEmail;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary />
public static class DiExtensions
{
    /// <summary />
    public static FluentEmailServicesBuilder AddResend( this FluentEmailServicesBuilder builder )
    {
        // TODO: HttpClient
        // TODO: Action over ResendClientOptions

        var sd1 = ServiceDescriptor.Transient<IResend>( ( sp ) =>
        {
            var opt = sp.GetRequiredService<IOptionsSnapshot<ResendClientOptions>>();
            var http = sp.GetRequiredService<HttpClient>();

            return new ResendClient( opt, http );
        } );

        var sd2 = ServiceDescriptor.Singleton<ISender>( ( sp ) =>
        {
            var resend = sp.GetRequiredService<IResend>();

            return new ResendSender( resend );
        } );

        builder.Services.Add( sd1 );
        builder.Services.Add( sd2 );

        return builder;
    }
}
