using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Discriminated union of byte array or string value, used to represent
/// attachments into the Resend API.
/// </summary>
[JsonConverter( typeof( ByteArrayOrStringConverter ) )]
public readonly struct ByteArrayOrString
{
    private readonly byte[]? _bytes;
    private readonly string? _string;


    /// <summary />
    public ByteArrayOrString( byte[] value )
    {
        _bytes = value;
        _string = null;
    }


    /// <summary />
    public ByteArrayOrString( string value )
    {
        _string = value;
        _bytes = null;
    }


    /// <summary>
    /// Gets whether the value is a byte array.
    /// </summary>
    public bool IsByteArray { get =>  _bytes != null; }

    /// <summary>
    /// Gets whether the value is a string.
    /// </summary>
    public bool IsString { get => _string != null; }


    /// <summary>
    /// Gets the byte array value, or null.
    /// </summary>
    public byte[]? ByteArray { get => _bytes; }


    /// <summary>
    /// Gets the string value, or null.
    /// </summary>
    public string? String { get => _string; }


    /// <summary>
    /// Gets the byte array value.
    /// </summary>
    /// <returns>
    /// Byte array value.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown if value isn't byte array.</exception>
    public byte[] AsByteArray()
    {
        if ( _bytes is not null )
            return _bytes;

        throw new InvalidOperationException( "Value is not a byte array." );
    }

    /// <summary>
    /// Gets the string value.
    /// </summary>
    /// <returns>
    /// String value.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown if value isn't string.</exception>
    public string AsString()
    {
        if ( _string is not null )
            return _string;

        throw new InvalidOperationException( "Value is not a string." );
    }


    /// <summary>
    /// Gets a 32-bit integer that represents the total number of bytes (if value is
    /// a byte array), or the number of characters (if value is a string).
    /// </summary>
    public int Length
    {
        get
        {
            if ( _bytes != null )
                return _bytes.Length;

            if ( _string != null )
                return _string.Length;

            return 0;
        }
    }


    /// <summary>
    /// Gets a 64-bit integer that represents the total number of bytes (if value is
    /// a byte array), or the number of characters (if value is a string).
    /// </summary>
    public long LongLength
    {
        get
        {
            if ( _bytes != null )
                return _bytes.LongLength;

            if ( _string != null )
                return _string.Length;

            return 0;
        }
    }


    /// <inheritdoc />
    public override string ToString()
    {
        if ( _bytes != null )
            return $"[byte[]: {_bytes.LongLength} bytes]";

        if ( _string != null )
            return _string;

        return "(empty)";
    }


    /// <summary>
    /// Implicitly cast, if possible, to a byte array.
    /// </summary>
    public static implicit operator byte[]( ByteArrayOrString value )
    {
        if ( value.ByteArray != null )
            return value.ByteArray;

        throw new InvalidOperationException( "BA002: Value is not a byte[]" );
    }


    /// <summary />
    public static implicit operator ByteArrayOrString( byte[] value )
    {
        return new ByteArrayOrString( value );
    }


    /// <summary />
    public static implicit operator ByteArrayOrString( string value )
    {
        return new ByteArrayOrString( value );
    }
}