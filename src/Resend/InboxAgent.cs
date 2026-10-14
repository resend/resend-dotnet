using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Agent settings of an inbox.
/// </summary>
/// <remarks>
/// An inbox without a configured agent returns empty settings: <see cref="Instructions"/>
/// and <see cref="Tone"/> are null and <see cref="EnabledActions"/> is empty.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-agent" />
public class InboxAgent
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Instructions the agent follows when it handles the threads of the inbox.
    /// </summary>
    /// <remarks>
    /// Null when no instructions are set.
    /// </remarks>
    [JsonPropertyName( "instructions" )]
    public string? Instructions { get; set; }

    /// <summary>
    /// Tone the agent writes in.
    /// </summary>
    /// <remarks>
    /// One of the presets <c>concise_professional</c>, <c>concise_casual</c>,
    /// <c>expanded_professional</c> or <c>expanded_casual</c>, or custom text in the
    /// owner's own words, such as <c>friendly and concise</c>. Older dashboard values
    /// such as <c>Concise · Casual</c> are custom text. Null when no tone is set.
    /// </remarks>
    [JsonPropertyName( "tone" )]
    public string? Tone { get; set; }

    /// <summary>
    /// Actions the agent can take.
    /// </summary>
    /// <remarks>
    /// Empty when no actions are enabled.
    /// </remarks>
    [JsonPropertyName( "enabled_actions" )]
    public List<InboxAgentAction> EnabledActions { get; set; } = new();
}
