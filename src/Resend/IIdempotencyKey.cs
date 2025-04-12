namespace Resend;

/// <summary />
public interface IIdempotencyKey
{
    /// <summary>
    /// Based on the payload, generate a string representation of a idempotency key.
    /// </summary>
    /// <remarks>
    /// Should be unique for the account. Length of key must be less than equal to 256 chars.
    /// </remarks>
    string ToKey();
}


/// <summary>
/// Idempotency key which is a <see cref="Guid" />.
/// </summary>
public record UuidIdempotencyKey : IIdempotencyKey
{
    /// <summary />
    public UuidIdempotencyKey( Guid value )
    {
        this.Uuid = value;
    }


    /// <summary />
    public Guid Uuid { get; }


    /// <summary />
    public string ToKey()
    {
        return this.Uuid.ToString();
    }
}


/// <summary>
/// Idempotency key which is a <see cref="String" />.
/// </summary>
public record StringIdempotencyKey : IIdempotencyKey
{
    /// <summary />
    public StringIdempotencyKey( string value )
    {
        this.Key = value;
    }


    /// <summary />
    public string Key { get; }


    /// <summary />
    public string ToKey()
    {
        return this.Key;
    }
}


/// <summary>
/// Idempotency key which is an entity/id tuple.
/// </summary>
public record EntityIdempotencyKey<T> : IIdempotencyKey
    where T : notnull
{
    /// <summary />
    public required string EntityType { get; set; }

    /// <summary />
    public required T EntityId { get; set; }


    /// <summary />
    public string ToKey()
    {
        return $"{this.EntityType}/{this.EntityId.ToString()}";
    }
}