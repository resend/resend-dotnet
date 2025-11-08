namespace Resend.DocsCheck;

/// <summary>
/// Code compilation result.
/// </summary>
public enum Result
{
    /// <summary>
    /// Code sample compiles successfully.
    /// </summary>
    Ok,

    /// <summary>
    /// Code sample not written.
    /// </summary>
    Warn,

    /// <summary>
    /// 
    /// </summary>
    Fail,
}