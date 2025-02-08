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

        // Note: There's a loss of precision when roundtripping through JSON
        Assert.Equal( src.Moment.Year, tgt.Moment.Year );
        Assert.Equal( src.Moment.Month, tgt.Moment.Month );
        Assert.Equal( src.Moment.Day, tgt.Moment.Day );
        Assert.Equal( src.Moment.Hour, tgt.Moment.Hour );
        Assert.Equal( src.Moment.Minute, tgt.Moment.Minute );
        Assert.Equal( src.Moment.Second, tgt.Moment.Second );
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