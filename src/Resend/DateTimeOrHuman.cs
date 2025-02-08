using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
[JsonConverter( typeof( DateTimeOrHumanConverter ) )]
public struct DateTimeOrHuman : IComparable<DateTimeOrHuman>
{
    private DateTime? _dateTime;
    private string? _human;


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
    public DateTime Moment
    {
        get
        {
            if ( _dateTime.HasValue == false )
                throw new InvalidOperationException();

            return _dateTime.Value;
        }

        set
        {
            _human = null;
            _dateTime = value;
        }
    }


    /// <summary />
    public string Human
    {
        get
        {
            if ( _human == null )
                throw new InvalidOperationException();

            return _human!;
        }

        set
        {
            _human = value;
            _dateTime = null;
        }
    }


    /// <summary />
    public override string ToString()
    {
        if ( this.IsMoment == true )
            return this.Moment.ToString();
        else
            return this.Human;
    }


    /// <summary />
    public int CompareTo( DateTimeOrHuman other )
    {
        if ( this.IsMoment != other.IsMoment )
            throw new NotSupportedException( "Cannot compare DateTimeOrHuman values of different kind" );

        if ( this.IsMoment == true )
            return this.Moment.CompareTo( other.Moment );
        else
            return this.Human!.CompareTo( other.Human );
    }


    /// <summary />
    public static implicit operator DateTime( DateTimeOrHuman value )
    {
        if ( value.IsMoment == true )
            return value.Moment;

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