using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Discriminated union of absolute moment or natural language timespan.
/// </summary>
[JsonConverter( typeof( DateTimeOrHumanConverter ) )]
public readonly struct DateTimeOrHuman : IComparable<DateTimeOrHuman>
{
    private readonly DateTime? _dateTime;
    private readonly string? _human;


    /// <summary>
    /// Value with absolute moment.
    /// </summary>
    public DateTimeOrHuman( DateTime moment )
    {
        _dateTime = moment;
        _human = null;
    }


    /// <summary>
    /// Value with natural language timespan.
    /// </summary>
    public DateTimeOrHuman( string human )
    {
        _human = human;
        _dateTime = null;
    }


    /// <summary>
    /// Gets whether current value is an (absolute) moment in date/time.
    /// </summary>
    public bool IsMoment { get => this._dateTime.HasValue; }

    /// <summary>
    /// Gets the current moment in date/time.
    /// </summary>
    public DateTime? Moment { get => _dateTime; }

    /// <summary>
    /// Gets the natural language timespan.
    /// </summary>
    public string? Human { get => _human; }


    /// <summary />
    public override string ToString()
    {
        if ( this.IsMoment == true )
            return this.Moment!.Value.ToString();
        else
            return this.Human!;
    }


    /// <inheritdoc/> />
    public int CompareTo( DateTimeOrHuman other )
    {
        if ( this.IsMoment != other.IsMoment )
            throw new NotSupportedException( "DH001: Cannot compare DateTimeOrHuman values of different kind" );

        if ( this.IsMoment == true )
            return this.Moment!.Value.CompareTo( other.Moment!.Value );
        else
            return this.Human!.CompareTo( other.Human );
    }


    /// <summary>
    /// Implicitly cast, if possible, to a moment.
    /// </summary>
    public static implicit operator DateTime( DateTimeOrHuman value )
    {
        if ( value.IsMoment == true )
            return value.Moment!.Value;

        throw new InvalidOperationException( "DH002: Value is not a DateTime" );
    }


    /// <summary>
    /// Implicitly create a monent in date/time value.
    /// </summary>
    public static implicit operator DateTimeOrHuman( DateTime value )
    {
        return new DateTimeOrHuman( value );
    }


    /// <summary>
    /// Implicitly create a natural language timespan value.
    /// </summary>
    public static implicit operator DateTimeOrHuman( string value )
    {
        return new DateTimeOrHuman( value );
    }
}