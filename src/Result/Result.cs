using Result.Abstractions;
using Result.Errors;

namespace Result;

public class Result<T> : IResult
{
    private readonly T? _value;

    public Result(T value)
    {
        _value = value;
    }

    public Result(IEnumerable<Error> errors)
    {
        Failure = [..errors];
    }

    public Result(params Error[] errors)
    {
        Failure = [..errors];
    }

    public Failure Failure { get; } = [];
    public bool IsFailed => Failure;
    public T Value => _value ?? throw new InvalidOperationException("result is unsuccessful and do not contains any value");

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    public void Deconstruct(out T? value, out Failure failure)
    {
        value = Value;
        failure = Failure;
    }

    public TProjection Match<TProjection>(Func<T, TProjection> success, Func<Failure, TProjection> failure)
    {
        return Failure
            ? failure(Failure)
            : success(Value);
    }

    public async Task<TProjection> MatchAsync<TProjection>(Func<T, Task<TProjection>> success, Func<Failure, TProjection> failure)
    {
        return Failure
            ? failure(Failure)
            : await success(Value);
    }
}
