using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class TemplateData
{
    /// <summary />
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "alias" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Alias { get; set; }

    /// <summary>
    /// Sender email address.
    /// </summary>
    [JsonPropertyName( "from" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddress? From { get; set; }

    /// <summary>
    /// Email subject.
    /// </summary>
    [JsonPropertyName( "subject" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Subject { get; set; }

    /// <summary>
    /// Reply-to email address.
    /// </summary>
    [JsonPropertyName( "reply_to" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? ReplyTo { get; set; }

    /// <summary />
    [JsonPropertyName( "html" )]
    public string HtmlBody { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "text" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TextBody { get; set; }

    /// <summary />
    [JsonPropertyName( "variables" )]
    public List<TemplateVariable>? Variables { get; set; }


    /// <summary>
    /// Adds a variable.
    /// </summary>
    /// <param name="variable">Variable.</param>
    /// <returns>Variable instance.</returns>
    public TemplateVariable AddVariable( TemplateVariable variable )
    {
        if ( this.Variables == null )
            this.Variables = new List<TemplateVariable>();

        this.Variables.Add( variable );

        return variable;
    }


    /// <summary>
    /// Adds a variable.
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="type">Type of variable</param>
    /// <param name="default">Default value.</param>
    /// <returns>Variable instance.</returns>
    public TemplateVariable AddVariable( string key, TemplateVariableType type, object? @default = null )
    {
        if ( this.Variables == null )
            this.Variables = new List<TemplateVariable>();

        var v = new TemplateVariable()
        {
            Key = key,
            Type = type,
            Default = @default,
        };

        this.Variables.Add( v );

        return v;
    }
}