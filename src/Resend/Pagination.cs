using System.Text.Json.Serialization;

namespace Resend;


/// <summary />
public class PaginatedQuery
{
    /// <summary />
    public int? Limit { get; set; }

    /// <summary />
    public string? BeforeId { get; set; }

    /// <summary />
    public string? AfterId { get; set; }
}


/// <summary />
public class PaginatedResult<T>
{
    /// <summary />
    [JsonPropertyName( "has_more" )]
    public required bool HasMore { get; set; }

    /// <summary />
    [JsonPropertyName( "data" )]
    public required List<T> Data { get; set; }
}