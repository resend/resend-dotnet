namespace Resend;

/// <summary />
public static class IdempotencyKey
{
    /// <summary />
    public static string New()
    {
        return Guid.NewGuid().ToString();
    }


    /// <summary />
    public static string New<T>( string entity, T entityId )
    {
        return $"{entity}/{entityId}";
    }
}