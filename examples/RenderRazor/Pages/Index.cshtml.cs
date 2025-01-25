using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RenderRazor.Pages;

/// <summary />
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    
    /// <summary />
    public IndexModel( ILogger<IndexModel> logger )
    {
        _logger = logger;
    }


    /// <summary />
    public void OnGet()
    {
    }
}