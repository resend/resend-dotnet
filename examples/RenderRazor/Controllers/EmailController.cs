using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;
using RenderRazor.Views.Email;
using Resend;
using System.Text;

namespace RenderRazor.Controllers;

/// <summary />
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IRazorViewEngine _razor;
    private readonly IServiceProvider _services;
    private readonly IResend _resend;


    /// <summary />
    public EmailController( IRazorViewEngine razor, IServiceProvider services, IResend resend, ILogger<EmailController> logger )
    {
        _razor = razor;
        _services = services;
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
        var viewEngineResult = _razor.GetView( "~/Views/Email/", $"{view}.cshtml", false );

        if ( viewEngineResult == null )
            throw new ApplicationException( "View not found" );

        if ( viewEngineResult.Success == false )
            throw new ApplicationException( "View failed" );


        /*
         * 
         */
        var actionContext = GetActionContext();


        /*
         * 
         */
        var sb = new StringBuilder();

        using ( var sw = new StringWriter( sb ) )
        {
            var tempDataSerializer = (TempDataSerializer) _services.GetService( typeof( TempDataSerializer ) )!;
            var tempDataProvider = new SessionStateTempDataProvider( tempDataSerializer );

            var viewContext = new ViewContext(
                actionContext,
                viewEngineResult.View,
                new ViewDataDictionary<T>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary()
                )
                {
                    Model = model
                },
                    new TempDataDictionary( actionContext.HttpContext, tempDataProvider ),
                sw,
                new HtmlHelperOptions()
            );

            await viewEngineResult.View.RenderAsync( viewContext );
        }


        return sb.ToString();
    }


    /// <summary />
    private ActionContext GetActionContext()
    {
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _services
        };

        return new ActionContext( httpContext, new RouteData(), new ActionDescriptor() );
    }
}
