namespace Result.Errors;
public enum ErrorType
{
    Failure = 0,
    Unexpected = 1,
    Forbidden = 2 << 0,
    Validation = 2 << 1,
    Conflict = 2 << 2,
    NotFound = 2 << 3,
}
