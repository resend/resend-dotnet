namespace Resend;

/// <summary>
/// Inbox, given by its identifier or by its email address, such as <c>support@acme.com</c>.
/// </summary>
/// <remarks>
/// Converts implicitly from a <see cref="Guid"/> and from a <see cref="string"/>, so every
/// inbox method accepts either. A value that contains <c>@</c> must be a valid email address,
/// and any other value must be a UUID, or the API returns a 422. The address match is
/// case-insensitive. An address that matches no inbox returns a 404.
/// </remarks>
public readonly struct InboxIdOrEmail
{
    private readonly string _value;


    /// <summary>
    /// Inbox given by its identifier.
    /// </summary>
    public InboxIdOrEmail( Guid inboxId )
    {
        _value = inboxId.ToString();
    }


    /// <summary>
    /// Inbox given by its identifier or by its email address.
    /// </summary>
    public InboxIdOrEmail( string inboxIdOrEmail )
    {
        /*
         * A blank value would target the collection endpoint, which answers with a list
         * that deserializes into an all-default object instead of failing.
         */
        ArgumentException.ThrowIfNullOrWhiteSpace( inboxIdOrEmail );

        _value = inboxIdOrEmail;
    }


    /// <summary>
    /// Path of the inbox, with an email address percent-encoded.
    /// </summary>
    internal string Path
    {
        get => "/inboxes/" + Uri.EscapeDataString( _value );
    }


    /// <inheritdoc />
    public override string ToString()
    {
        return _value;
    }


    /// <summary>
    /// Implicitly create a value from an inbox identifier.
    /// </summary>
    public static implicit operator InboxIdOrEmail( Guid inboxId )
    {
        return new InboxIdOrEmail( inboxId );
    }


    /// <summary>
    /// Implicitly create a value from an inbox identifier or email address.
    /// </summary>
    public static implicit operator InboxIdOrEmail( string inboxIdOrEmail )
    {
        return new InboxIdOrEmail( inboxIdOrEmail );
    }
}
