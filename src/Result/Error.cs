namespace Result;

/// <summary>
/// Represents an error.
/// </summary>
public class Error
{
    public Error()
    {
    }

    public Error(string message)
    {
        Message = message;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public string? Message { get; }

    public override string ToString()
    {
        return Message ?? string.Empty;
    }
}
