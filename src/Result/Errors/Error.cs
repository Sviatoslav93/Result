namespace Result.Errors;

public readonly record struct Error(string Code, string Description, ErrorType ErrorType = ErrorType.Failure)
{
    public static Error Failure(string code, string description)
    {
        return new Error(code, description);
    }

    public static Error Unexpected(string code, string description)
    {
        return new Error(code, description, ErrorType.Unexpected);
    }

    public static Error Forbidden(string code, string description)
    {
        return new Error(code, description, ErrorType.Forbidden);
    }

    public static Error Validation(string code, string description)
    {
        return new Error(code, description, ErrorType.Validation);
    }

    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }

    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }
}
