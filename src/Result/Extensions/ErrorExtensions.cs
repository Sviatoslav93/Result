using Result.Errors;

namespace Result.Extensions;

public static class ErrorExtensions
{
    public static Failure ToFailure(this IEnumerable<Error> errors)
    {
        return new Failure(errors);
    }

    public static Failure ToFailure(this IEnumerable<Error> errors, out Failure failure)
    {
        failure = new Failure(errors);
        return failure;
    }
}
