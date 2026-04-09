using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.AutomationRunListAsync"/>.
/// </summary>
public class AutomationRunListQuery : PaginatedQuery
{
    /// <summary>
    /// Comma-separated run statuses: <c>running</c>, <c>completed</c>, <c>failed</c>, <c>cancelled</c>.
    /// </summary>
    [JsonIgnore]
    public string? Status { get; set; }
}
