namespace WorkerService;

/// <summary />
public class EmailWorkerOptions
{
    /// <summary />
    public string From { get; set; } = default!;

    /// <summary />
    public string To { get; set; } = default!;

    /// <summary />
    public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes( 1 );
}
