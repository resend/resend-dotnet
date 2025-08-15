#if NETSTANDARD2_0_OR_GREATER

namespace Resend.NetStandard2;

/// <summary />
public class HttpMethod
{
    /// <summary />
    public static readonly System.Net.Http.HttpMethod Delete = System.Net.Http.HttpMethod.Delete;

    /// <summary />
    public static readonly System.Net.Http.HttpMethod Get = System.Net.Http.HttpMethod.Get;

    /// <summary />
    public static readonly System.Net.Http.HttpMethod Patch = new System.Net.Http.HttpMethod( "PATCH" );

    /// <summary />
    public static readonly System.Net.Http.HttpMethod Post = System.Net.Http.HttpMethod.Post;
}

#endif