namespace Resend;

/// <summary>
/// Pagination query for endpoints that support forward pagination only.
/// </summary>
public class PaginatedAfterQuery
{
    /// <summary>
    /// Number of records to return.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Cursor after which records are returned.
    /// </summary>
    public string? After { get; set; }
}
