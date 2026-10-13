using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update the agent settings of an inbox. Provide at least one of
/// <see cref="Instructions"/>, <see cref="Tone"/> or <see cref="EnabledActions"/>.
/// </summary>
/// <remarks>
/// A property that is not assigned is left out of the request and keeps its current value.
/// Assigning null to <see cref="Instructions"/> or <see cref="Tone"/> sends null, which
/// clears it.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-agent" />
[JsonConverter( typeof( InboxAgentUpdateDataConverter ) )]
public class InboxAgentUpdateData
{
    private readonly HashSet<string> _assigned = new();
    private string? _instructions;
    private string? _tone;


    /// <summary>
    /// Instructions the agent follows when it handles the threads of the inbox.
    /// </summary>
    /// <remarks>
    /// Max 4000 characters. Leading and trailing whitespace is trimmed, and the trimmed
    /// value must not be empty. Assign null to clear the instructions.
    /// </remarks>
    public string? Instructions
    {
        get => _instructions;
        set { _instructions = value; _assigned.Add( "instructions" ); }
    }

    /// <summary>
    /// Tone the agent writes in, such as <c>friendly and concise</c>.
    /// </summary>
    /// <remarks>
    /// Max 64 characters. Leading and trailing whitespace is trimmed, and the trimmed value
    /// must not be empty. Assign null to clear the tone.
    /// </remarks>
    public string? Tone
    {
        get => _tone;
        set { _tone = value; _assigned.Add( "tone" ); }
    }

    /// <summary>
    /// Actions the agent can take. Replaces the whole set.
    /// </summary>
    /// <remarks>
    /// Duplicates are removed. Assign an empty list to disable all actions. Null is left out
    /// of the request.
    /// </remarks>
    public List<InboxAgentAction>? EnabledActions { get; set; }


    /// <summary />
    internal bool IsAssigned( string propertyName )
    {
        return _assigned.Contains( propertyName );
    }
}
