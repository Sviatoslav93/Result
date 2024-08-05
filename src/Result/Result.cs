using Result.Errors;

public class Result<T> : IResult
{
    private readonly Failure _failure = [];
    private readonly T? _value;
    private Result(T? value, IEnumerable<Error> errors)
    {
        _value = value;
        _failure.AddRange(errors);
    }

    private Result(T value)
        : this(value, [])
    {
    }

    private Result(Error error)
        : this(default, [error])
    {
    }

    private Result(IEnumerable<Error> errors)
        : this(default, errors)
    {
    }

    public Failure Failure => _failure;
    public T Value => _value ?? throw new InvalidOperationException("result is unsuccessful and do not contains any value");
    public bool IsFailed => Failure;

    public static implicit operator Result<T>(T value) => new Result<T>(value);
    public static implicit operator Result<T>(Error error) => new Result<T>(error);
    public static implicit operator Result<T>(Failure failure) => new Result<T>(failure);

    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failed(Error error) => new Result<T>(error);

    public TProjection Match<TProjection>(Func<T, TProjection> success, Func<Failure, TProjection> failure) => Failure
            ? failure(Failure)
            : success(Value);

    public async Task<TProjection> MatchAsync<TProjection>(Func<T, Task<TProjection>> success, Func<Failure, TProjection> failure) => Failure
            ? failure(Failure)
            : await success(Value);

    public void Deconstruct(out T? value, out Failure failure)
    {
        value = Value;
        failure = Failure;
    }
}
