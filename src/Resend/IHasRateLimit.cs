namespace Resend
{
    public interface IHasRateLimit
    {
        ResendRateLimit? Limits { get; }
    }
}