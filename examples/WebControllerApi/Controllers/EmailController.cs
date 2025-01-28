using Microsoft.AspNetCore.Mvc;
using Resend;
using System.ComponentModel.DataAnnotations;

namespace WebControllerApi.Controllers;

/// <summary />
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IResend _resend;
    private readonly ILogger<EmailController> _logger;


    /// <summary />
    public EmailController( IResend resend, ILogger<EmailController> logger )
    {
        _resend = resend;
        _logger = logger;
    }


    /// <summary>
    /// Sends a pre-defined email.
    /// </summary>
    [HttpGet]
    [Route( "email/send" )]
    public async Task<string> EmailSendFixed()
    {
        /*
         * 
         */
        var message = new EmailMessage();
        message.From = "you@domain.com";
        message.To.Add( "user@gmail.com" );
        message.Subject = "Hello from Controller API";
        message.TextBody = "Email using Resend .NET SDK";

        var resp = await _resend.EmailSendAsync( message );

        _logger.LogInformation( "Sent email, with Id = {EmailId}", resp.Content );

        return resp.Content.ToString();
    }


    /// <summary>
    /// Sends an email to specified email address.
    /// </summary>
    [HttpPost]
    [Route( "email/send" )]
    public async Task<ActionResult<string>> EmailSend( [FromBody] EmailSendRequest request )
    {
        /*
         * Validate
         */
        if ( ModelState.IsValid == false )
        {
            return BadRequest();
        }


        /*
         * 
         */
        var message = new EmailMessage();
        message.From = "you@domain.com";
        message.To.Add( request.To );
        message.Subject = request.Subject ?? "Hello from Web Controller";
        message.TextBody = "Email using Resend .NET SDK";

        var resp = await _resend.EmailSendAsync( message );

        _logger.LogInformation( "Sent email to {To}, with Id = {EmailId}", request.To, resp.Content );

        return resp.Content.ToString();
    }


    /// <summary>
    /// Request payload.
    /// </summary>
    public class EmailSendRequest
    {
        /// <summary>
        /// Email address of recipient.
        /// </summary>
        [Required]
        [EmailAddress]
        public string To { get; set; } = default!;

        /// <summary>
        /// Subject.
        /// </summary>
        [StringLength( 100, MinimumLength = 1 )]
        public string? Subject { get; set; }
    }
}