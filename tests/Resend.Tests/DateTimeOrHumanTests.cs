using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend.Tests;

/// <summary />
public class DateTimeOrHumanTests
{
    /// <summary />
    public class WrapperClass
    {
        /// <summary />
        [JsonPropertyName( "prop" )]
        public DateTimeOrHuman? Moment { get; set; }
    }


    /// <summary />
    [Fact]
    public void DateTimeToString()
    {
        var dt = DateTime.UtcNow;
        DateTimeOrHuman src = dt;

        Assert.Equal( dt.ToString(), src.ToString() );
    }


    /// <summary />
    [Fact]
    public void DateTimeCast()
    {
        var dt = DateTime.UtcNow;
        DateTimeOrHuman src = dt;
        DateTime tgt = src;

        Assert.Equal( dt, tgt );
    }


    /// <summary />
    [Fact]
    public void DateTimeCastFromHuman()
    {
        DateTimeOrHuman src = "in 5 mins";

        Action act = () =>
        {
            DateTime tgt = src;
        };

        var ex = Assert.Throws<InvalidOperationException>( act );
        Assert.NotNull( ex.Message );
        Assert.StartsWith( "DH002:", ex.Message );
    }


    /// <summary />
    [Fact]
    public void DateTimeComparable()
    {
        var dt1 = DateTime.UtcNow;
        var dt2 = DateTime.UtcNow.AddDays( 1 );

        DateTimeOrHuman src1 = dt1;
        DateTimeOrHuman src2 = dt2;

        Assert.Equal( dt1.CompareTo( dt2 ), src1.CompareTo( src2 ) );
    }


    /// <summary />
    [Fact]
    public void HumanToString()
    {
        var str = "in 5 mins";
        DateTimeOrHuman v = str;

        Assert.Equal( str, v.ToString() );
    }


    /// <summary />
    [Fact]
    public void HumanComparable()
    {
        var str1 = "in 5 mins";
        var str2 = "tomorrow 5:00 am";

        DateTimeOrHuman v1 = str1;
        DateTimeOrHuman v2 = str2;

        Assert.Equal( str1.CompareTo( str2 ), v1.CompareTo( v2 ) );
    }


    /// <summary />
    [Fact]
    public void MixedComparable()
    {
        var str = "in 5 mins";
        var dt = DateTime.UtcNow;

        DateTimeOrHuman v1 = str;
        DateTimeOrHuman v2 = dt;

        Action act = () => v1.CompareTo( v2 );

        var ex = Assert.Throws<NotSupportedException>( act );
        Assert.NotNull( ex.Message );
        Assert.StartsWith( "DH001:", ex.Message );
    }


    /// <summary />
    [Fact]
    public void PropertyNotNull()
    {
        var src = new WrapperClass()
        {
            Moment = DateTime.UtcNow,
        };

        var json = JsonSerializer.Serialize( src );

        var tgt = JsonSerializer.Deserialize<WrapperClass>( json );

        Assert.NotNull( tgt );
        Assert.NotNull( tgt.Moment );
        Assert.True( tgt.Moment.Value.IsMoment );
    }


    /// <summary />
    [Fact]
    public void PropertyNull()
    {
        var src = new WrapperClass()
        {
            Moment = null,
        };

        var json = JsonSerializer.Serialize( src );

        var tgt = JsonSerializer.Deserialize<WrapperClass>( json );

        Assert.NotNull( tgt );
        Assert.Null( tgt.Moment );
    }


    /// <summary />
    [Fact]
    public void DateTimeOverJson()
    {
        var src = (DateTimeOrHuman) DateTime.UtcNow;

        var json = JsonSerializer.Serialize( src );

        var tgt = JsonSerializer.Deserialize<DateTimeOrHuman>( json );

        Assert.True( tgt.IsMoment );
        Assert.True( tgt.Moment.HasValue );

        // 
        var srcv = src.Moment!.Value;
        var tgtv = tgt.Moment!.Value;

        // Note: There's a loss of precision when roundtripping through JSON
        Assert.Equal( srcv.Year, tgtv.Year );
        Assert.Equal( srcv.Month, tgtv.Month );
        Assert.Equal( srcv.Day, tgtv.Day );
        Assert.Equal( srcv.Hour, tgtv.Hour );
        Assert.Equal( srcv.Minute, tgtv.Minute );
        Assert.Equal( srcv.Second, tgtv.Second );
    }


    /// <summary />
    [Theory]
    [InlineData( "in 5 mins" )]
    [InlineData( "tomorrow at 9am" )]
    [InlineData( "Friday at 3pm ET" )]
    [InlineData( "9am tomorrow" )]
    public void StringOverJson( string human )
    {
        var src = (DateTimeOrHuman) human;

        var json = JsonSerializer.Serialize( src );

        var tgt = JsonSerializer.Deserialize<DateTimeOrHuman>( json );

        Assert.False( tgt.IsMoment );
        Assert.Equal( src, tgt );
        Assert.Equal( human, tgt.Human );
    }
}