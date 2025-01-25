using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace RenderRazor.Pages;

/// <summary />
[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    /// <summary />
    public string? RequestId { get; set; }

    /// <summary />
    public bool ShowRequestId => !string.IsNullOrEmpty( RequestId );

    private readonly ILogger<ErrorModel> _logger;


    /// <summary />
    public ErrorModel( ILogger<ErrorModel> logger )
    {
        _logger = logger;
    }


    /// <summary />
    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
