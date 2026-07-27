using System.Reflection;

namespace Resend;

/// <summary>
/// Wire values of <typeparamref name="T"/>, as declared by <see cref="JsonStringValueAttribute"/>.
/// </summary>
internal static class JsonStringEnumValue<T>
    where T : struct, Enum
{
    /// <summary />
    static JsonStringEnumValue()
    {
        var tt = typeof( T );

        var names = tt.GetEnumNames();
        var values = tt.GetEnumValues();

        var fwd = new Dictionary<T, string>( names.Length );
        var rev = new Dictionary<string, T>( names.Length );

        for ( var i = 0; i < names.Length; i++ )
        {
            var name = names[ i ];
            var field = tt.GetField( name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static )!;

            var str = field.GetCustomAttribute<JsonStringValueAttribute>()?.Value ?? name;
            var val = (T) values.GetValue( i )!;

            // Aliases -- several names sharing one underlying value -- are a legal enum
            // declaration, so the first name encountered supplies the wire value rather
            // than the alias making the type unusable.
            if ( fwd.ContainsKey( val ) == false )
                fwd.Add( val, str );

            if ( rev.TryGetValue( str, out var prev ) == false )
                rev.Add( str, val );
            else if ( EqualityComparer<T>.Default.Equals( prev, val ) == false )
                throw new InvalidOperationException( $"SE004: '{tt.Name}' maps wire value '{str}' to both '{prev}' and '{val}'" );
        }

        Forward = fwd;
        Reverse = rev;
    }


    /// <summary />
    public static IReadOnlyDictionary<T, string> Forward { get; }


    /// <summary />
    public static IReadOnlyDictionary<string, T> Reverse { get; }


    /// <summary>
    /// Wire value of <paramref name="value"/>, for use outside of JSON serialization -- eg. in
    /// query string parameters.
    /// </summary>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Wire value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="value"/> is not a member of <typeparamref name="T"/>.
    /// </exception>
    public static string Of( T value )
    {
        if ( Forward.TryGetValue( value, out var str ) == false )
            throw new ArgumentOutOfRangeException( nameof( value ), $"Invalid '{typeof( T ).Name}' value: '{value}'" );

        return str;
    }
}
