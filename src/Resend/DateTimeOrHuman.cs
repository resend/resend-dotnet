using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
[JsonConverter( typeof( DateTimeOrHumanConverter ) )]
public readonly struct DateTimeOrHuman : IComparable<DateTimeOrHuman>
{
    private readonly DateTime? _dateTime;
    private readonly string? _human;


    /// <summary />
    public DateTimeOrHuman( DateTime moment )
    {
        _dateTime = moment;
        _human = null;
    }


    /// <summary />
    public DateTimeOrHuman( string human )
    {
        _human = human;
        _dateTime = null;
    }


    /// <summary />
    public bool IsMoment { get => this._dateTime.HasValue; }

    /// <summary />
    public DateTime? Moment { get => _dateTime; }

    /// <summary />
    public string? Human { get => _human; }


    /// <summary />
    public override string ToString()
    {
        if ( this.IsMoment == true )
            return this.Moment!.Value.ToString();
        else
            return this.Human!;
    }


    /// <summary />
    public int CompareTo( DateTimeOrHuman other )
    {
        if ( this.IsMoment != other.IsMoment )
            throw new NotSupportedException( "Cannot compare DateTimeOrHuman values of different kind" );

        if ( this.IsMoment == true )
            return this.Moment!.Value.CompareTo( other.Moment!.Value );
        else
            return this.Human!.CompareTo( other.Human );
    }


    /// <summary />
    public static implicit operator DateTime( DateTimeOrHuman value )
    {
        if ( value.IsMoment == true )
            return value.Moment!.Value;

        throw new InvalidOperationException( "Value is not a DateTime" );
    }


    /// <summary />
    public static implicit operator DateTimeOrHuman( DateTime value )
    {
        return new DateTimeOrHuman( value );
    }


    /// <summary />
    public static implicit operator DateTimeOrHuman( string value )
    {
        return new DateTimeOrHuman( value );
    }
}