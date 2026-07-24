using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.SuppressionListAsync"/>.
/// </summary>
public class SuppressionListQuery : PaginatedQuery
{
    /// <summary>
    /// Filter by the origin of the suppression.
    /// </summary>
    [JsonIgnore]
    public SuppressionOrigin? Origin { get; set; }
}
