using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend.Tests;

/// <summary />
public class DateTimeCultureTests
{
    /// <summary />
    public class WrapperClass
    {
        /// <summary />
        [JsonPropertyName( "prop" )]
        [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
        public DateTime Moment { get; set; }
    }


    private static void WithCulture( string name, Action act )
    {
        var previous = CultureInfo.CurrentCulture;

        try
        {
            CultureInfo.CurrentCulture = new CultureInfo( name );
            act();
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }


    /// <summary />
    [Theory]
    [InlineData( "th-TH" )]
    [InlineData( "fa-IR" )]
    [InlineData( "fi-FI" )]
    public void DateTimeOrHumanWritesIsoUtcInAnyCulture( string culture )
    {
        WithCulture( culture, () =>
        {
            var moment = new DateTime( 2026, 9, 29, 13, 45, 10, DateTimeKind.Utc );

            var json = JsonSerializer.Serialize( (DateTimeOrHuman) moment );

            Assert.Equal( "\"2026-09-29T13:45:10Z\"", json );
        } );
    }


    /// <summary />
    [Theory]
    [InlineData( "th-TH" )]
    [InlineData( "fa-IR" )]
    [InlineData( "fi-FI" )]
    public void UtcDateTimeConverterWritesIsoUtcInAnyCulture( string culture )
    {
        WithCulture( culture, () =>
        {
            var src = new WrapperClass()
            {
                Moment = new DateTime( 2026, 9, 29, 13, 45, 10, DateTimeKind.Utc ),
            };

            var json = JsonSerializer.Serialize( src );

            Assert.Equal( "{\"prop\":\"2026-09-29T13:45:10Z\"}", json );
        } );
    }
}
