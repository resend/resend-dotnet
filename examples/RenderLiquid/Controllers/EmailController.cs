using Fluid;
using Microsoft.AspNetCore.Mvc;
using RenderLiquid.Email;
using Resend;

namespace RenderRazor.Controllers;

/// <summary />
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IResend _resend;


    /// <summary />
    public EmailController( IResend resend, ILogger<EmailController> logger )
    {
        _resend = resend;
        _logger = logger;
    }


    /// <summary />
    [HttpGet]
    [Route( "email/render" )]
    public async Task<string> EmailRender()
    {
        var html = await RenderEmail( "Hello", new HelloModel()
        {
            DisplayName = "John Doe",
            Items = { "NodeJs", "Go", "and many more..." },
            SenderName = "Resend Team",
        } );

        return html;
    }


    /// <summary />
    [HttpGet]
    [Route( "email/send" )]
    public async Task<string> EmailSend()
    {
        /*
         * 
         */
        var html = await RenderEmail( "Hello", new HelloModel()
        {
            DisplayName = "John Doe",
            Items = { "NodeJs", "Go", "and many more..." },
            SenderName = "Resend Team",
        } );


        /*
         * 
         */
        var message = new EmailMessage();
        message.From = "you@domain.com";
        message.To.Add( "user@gmail.com" );
        message.Subject = "Hello from Render Razor";
        message.HtmlBody = html;

        var resp = await _resend.EmailSendAsync( message );

        _logger.LogInformation( "Sent email, with Id = {EmailId}", resp.Content );

        return resp.Content.ToString();
    }


    /// <summary />
    public async Task<string> RenderEmail<T>( string view, T model )
    {
        /*
         * 
         */
        var type = typeof( T );
        var resx = $"{type.Namespace}.{view}.liquid";
        string source;

        using ( var stream = type.Assembly.GetManifestResourceStream( resx ) )
        {
            if ( stream == null )
                throw new ArgumentNullException( $"No .liquid view for '{view}'" );

            using ( var sr = new StreamReader( stream ) )
            {
                source = sr.ReadToEnd();
            }
        }


        /*
         * Note: As per documentation:
         * - `FluidParser` is thread-safe and should be shared by entire application.
         * - `IFluidTemplate` is thread-safe and should be cached.
         * - `TemplateContext` is not thread-safe.
         * 
         * As such, in your implementation, consider creating some infrastructure
         * that would hold a singleton parser and cache the templates.
         */
        var parser = new FluidParser();

        if ( parser.TryParse( source, out var template, out var error ) == false )
            throw new ApplicationException( $"Error parsing template: {error}" );

        var ctx = new TemplateContext( model );

        return await template.RenderAsync( ctx );
    }
}
