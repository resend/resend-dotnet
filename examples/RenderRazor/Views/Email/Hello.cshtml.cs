namespace RenderRazor.Views.Email;

/// <summary />
public class HelloModel
{
    /// <summary />
    public string? DisplayName { get; set; }

    /// <summary />
    public List<string> Items { get; set; } = new List<string>();

    /// <summary />
    public string? SenderName { get; set; }
}