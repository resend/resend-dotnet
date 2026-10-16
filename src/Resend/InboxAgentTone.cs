using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Tone preset that the agent of an inbox writes in.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-agent" />
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxAgentTone> ) )]
public enum InboxAgentTone
{
    /// <summary>
    /// Concise and professional.
    /// </summary>
    [JsonStringValue( "concise_professional" )]
    ConciseProfessional,

    /// <summary>
    /// Concise and casual.
    /// </summary>
    [JsonStringValue( "concise_casual" )]
    ConciseCasual,

    /// <summary>
    /// Expanded and professional.
    /// </summary>
    [JsonStringValue( "expanded_professional" )]
    ExpandedProfessional,

    /// <summary>
    /// Expanded and casual.
    /// </summary>
    [JsonStringValue( "expanded_casual" )]
    ExpandedCasual,
}
