using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend.Tests;

/// <summary />
public class JsonStringEnumValueTests
{
    /// <summary />
    [JsonConverter( typeof( JsonStringEnumValueConverter<TestEnum> ) )]
    public enum TestEnum
    {
        /// <summary />
        [JsonStringValue( "vee-one" )]
        ValueOne,

        /// <summary />
        [JsonStringValue( "ValueOne" )]
        WrongOne,

        /// <summary />
        ValueTwo,
    }


    /// <summary />
    [JsonConverter( typeof( JsonStringEnumValueConverter<OtherEnum> ) )]
    public enum OtherEnum
    {
        /// <summary />
        [JsonStringValue( "vee-one" )]
        V1,

        /// <summary />
        [JsonStringValue( "vee-two" )]
        V2,
    }


    /// <summary />
    [JsonConverter( typeof( JsonStringEnumValueConverter<AliasedEnum> ) )]
    public enum AliasedEnum
    {
        /// <summary />
        [JsonStringValue( "current" )]
        Current = 1,

        /// <summary />
        [JsonStringValue( "legacy" )]
        Legacy = 1,

        /// <summary />
        [JsonStringValue( "other" )]
        Other = 2,
    }


    /// <summary />
    [JsonConverter( typeof( JsonStringEnumValueConverter<ConflictingEnum> ) )]
    public enum ConflictingEnum
    {
        /// <summary />
        [JsonStringValue( "same" )]
        First = 1,

        /// <summary />
        [JsonStringValue( "same" )]
        Second = 2,
    }


    /// <summary />
    [Fact]
    public void AliasIsUsable()
    {
        var json = JsonSerializer.Serialize( AliasedEnum.Current );
        Assert.Contains( json, new[] { "\"current\"", "\"legacy\"" } );

        Assert.Equal( AliasedEnum.Current, JsonSerializer.Deserialize<AliasedEnum>( "\"current\"" ) );
        Assert.Equal( AliasedEnum.Current, JsonSerializer.Deserialize<AliasedEnum>( "\"legacy\"" ) );
        Assert.Equal( AliasedEnum.Other, JsonSerializer.Deserialize<AliasedEnum>( "\"other\"" ) );
    }


    /// <summary />
    [Fact]
    public void ConflictingWireValueThrows()
    {
        Action act = () => JsonSerializer.Serialize( ConflictingEnum.First );

        var ex = Assert.Throws<TypeInitializationException>( act );
        Assert.NotNull( ex.InnerException );
        Assert.StartsWith( "SE004:", ex.InnerException.Message );
    }


    /// <summary />
    [Fact]
    public void WithAttribute()
    {
        var src = TestEnum.ValueOne;

        var json = JsonSerializer.Serialize( src );
        Assert.Equal( "\"vee-one\"", json );

        var tgt = JsonSerializer.Deserialize<TestEnum>( json );
        Assert.Equal( TestEnum.ValueOne, tgt );
    }


    /// <summary />
    [Fact]
    public void WithoutAttribute()
    {
        var src = TestEnum.ValueTwo;

        var json = JsonSerializer.Serialize( src );
        Assert.Equal( "\"ValueTwo\"", json );

        var tgt = JsonSerializer.Deserialize<TestEnum>( json );
        Assert.Equal( TestEnum.ValueTwo, tgt );
    }


    /// <summary />
    [Fact]
    public void OtherAttribute()
    {
        var src = TestEnum.ValueOne;

        var json = JsonSerializer.Serialize( src );
        Assert.Equal( "\"vee-one\"", json );

        var tgt = JsonSerializer.Deserialize<OtherEnum>( json );
        Assert.Equal( OtherEnum.V1, tgt );
    }


    /// <summary />
    [Fact]
    public void FromNumber()
    {
        var json = "1";

        Action act = () => JsonSerializer.Deserialize<TestEnum>( json );

        var ex = Assert.Throws<JsonException>( act );
        Assert.NotNull( ex.Path );
        Assert.NotNull( ex.LineNumber );
        Assert.NotNull( ex.BytePositionInLine );
        Assert.StartsWith( "SE001:", ex.Message );
    }


    /// <summary />
    [Fact]
    public void FromInvalid()
    {
        var json = "\"xpto\"";

        Action act = () => JsonSerializer.Deserialize<TestEnum>( json );

        var ex = Assert.Throws<JsonException>( act );
        Assert.NotNull( ex.Path );
        Assert.NotNull( ex.LineNumber );
        Assert.NotNull( ex.BytePositionInLine );
        Assert.StartsWith( "SE002:", ex.Message );
    }
}
