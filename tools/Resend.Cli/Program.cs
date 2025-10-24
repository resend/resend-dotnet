using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Resend.Cli;

/// <summary />
[Command( "resend", Description = "Command-line tool for Resend API" )]
[Subcommand( typeof( ApiKeyCommand ) )]
[Subcommand( typeof( AudienceCommand ) )]
[Subcommand( typeof( BroadcastCommand ) )]
[Subcommand( typeof( ContactCommand ) )]
[Subcommand( typeof( DomainCommand ) )]
[Subcommand( typeof( EmailCommand ) )]
[Subcommand( typeof( TemplateCommand ) )]
[Subcommand( typeof( TopicCommand ) )]
[Subcommand( typeof( WebhookCommand ) )]
[HelpOption]
[VersionOptionFromMember( MemberName = nameof( GetVersion ) )]
public class Program
{
    /// <summary />
    public static int Main( string[] args )
    {
        /*
         * 
         */
        var app = new CommandLineApplication<Program>();

        var svc = new ServiceCollection();
        svc.AddOptions();
        svc.Configure<ResendClientOptions>( o =>
        {
            var apiUrl = Environment.GetEnvironmentVariable( "RESEND_APIURL" );

            if ( apiUrl != null )
                o.ApiUrl = apiUrl;

            o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
        } );
        svc.AddHttpClient<ResendClient>();
        svc.AddTransient<IResend, ResendClient>();

        var sp = svc.BuildServiceProvider();


        /*
         * 
         */
        try
        {
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection( sp );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( "ftl: unhandled exception during setup" );
            Console.WriteLine( ex.ToString() );

            return 2;
        }


        /*
         * 
         */
        try
        {
            return app.Execute( args );
        }
        catch ( UnrecognizedCommandParsingException ex )
        {
            Console.WriteLine( "err: " + ex.Message );

            return 2;
        }
        catch ( ResendException ex )
        {
            Console.WriteLine( "err: Resend API returned an error" );

            if ( ex.StatusCode != null )
                Console.WriteLine( "Status Code = {0} {1}", (int) ex.StatusCode, ex.StatusCode );

            Console.WriteLine( " Error Type = {0}", ex.ErrorType );
            Console.WriteLine( "   Message  = {0}", ex.Message );

            return 3;
        }
        catch ( Exception ex )
        {
            Console.WriteLine( "ftl: unhandled exception during execution" );
            Console.WriteLine( ex.ToString() );

            return 2;
        }
    }


    /// <summary />
    private static string GetVersion()
    {
        return typeof( Program ).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
    }


    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}
