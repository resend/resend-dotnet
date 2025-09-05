using System.Text.Json.Serialization;

namespace Resend;


/// <summary />
public class PaginatedQuery
{
    /// <summary>
    /// Number of records to be returned.
    /// </summary>
    /// <remarks>
    /// Minimum 1, Default 20, Maximum 100.
    /// </remarks>
    public int? Limit { get; set; }

    /// <summary>
    /// A cursor value after which records shall be returned.
    /// </summary>
    /// <remarks>
    /// A resource which matches this value shall not be included
    /// in the results.
    /// </remarks>
    public string? Before { get; set; }

    /// <summary>
    /// A cursor value before which records shall be returned.
    /// </summary>
    /// <remarks>
    /// A resource which matches this value shall not be included
    /// in the results.
    /// </remarks>
    public string? After { get; set; }
}


/// <summary />
public class PaginatedResult<T>
{
    /// <summary>
    /// Whether there are any more resources beyond the requested limit.
    /// </summary>
    [JsonPropertyName( "has_more" )]
    public required bool HasMore { get; set; }

    /// <summary>
    /// Page of resources matching the query criteria.
    /// </summary>
    [JsonPropertyName( "data" )]
    public required List<T> Data { get; set; }
}