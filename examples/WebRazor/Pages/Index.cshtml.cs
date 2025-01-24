using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resend;
using System.ComponentModel.DataAnnotations;

namespace WebRazor.Pages
{
    /// <summary />
    public class IndexModel : PageModel
    {
        private readonly IResend _resend;
        private readonly ILogger<IndexModel> _logger;


        /// <summary />
        public IndexModel( IResend resend, ILogger<IndexModel> logger )
        {
            _resend = resend;
            _logger = logger;
        }


        /// <summary />
        public void OnGet()
        {
            this.From = "you@domain.com";
            this.To = "user@gmail.com";
            this.Subject = "Hello from Razor";
            this.TextBody = "Email using Resend .NET SDK";
        }


        /// <summary />
        [BindProperty]
        [Required]
        public string? From { get; set; }
        
        /// <summary />
        [BindProperty]
        [Required]
        public string? To { get; set; }

        /// <summary />
        [BindProperty]
        [Required]
        public string? Subject { get; set; }

        /// <summary />
        [BindProperty]
        [Required]
        public string? TextBody { get; set; }


        /// <summary />
        [TempData]
        public string? SentTo { get; set; }

        /// <summary />
        [TempData]
        public string? EmailId { get; set; }


        /// <summary />
        public async Task<IActionResult> OnPost()
        {
            if ( ModelState.IsValid == false )
            {
                _logger.LogWarning( "Page is invalid" );
                return Page();
            }


            /*
             * 
             */
            var message = new EmailMessage();
            message.From = this.From!;
            message.To.Add( this.To! );
            message.Subject = this.Subject!;
            message.TextBody = this.TextBody;

            var resp = await _resend.EmailSendAsync( message );


            /*
             * 
             */
            this.SentTo = this.To!;
            this.EmailId = resp.Content.ToString().ToLowerInvariant();

            return RedirectToPage( "./EmailSent" );
        }
    }
}
