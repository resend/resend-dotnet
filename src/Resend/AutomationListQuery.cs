using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.AutomationListAsync"/>.
/// </summary>
public class AutomationListQuery : PaginatedQuery
{
    /// <summary>
    /// Filter by <c>enabled</c> or <c>disabled</c>.
    /// </summary>
    [JsonIgnore]
    public string? Status { get; set; }
}
